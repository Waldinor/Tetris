using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // setting tiles from images
        private readonly ImageSource[] tiles = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative))
        };

        // setting images of tetramino from images
        private readonly ImageSource[] tetraminoImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Empty-mino.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/I-mino.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/J-mino.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/L-mino.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Square-mino.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/S-mino.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/T-mino.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Z-mino.png", UriKind.Relative)),
        };

        private readonly Image[,] imageControls;
        private readonly int maxDelay = 1000;
        private readonly int minDelay = 75;
        private readonly int delayDecrease = 25;

        // creating a game object
        private GameState game = new GameState();
        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(game.grid);
        }

        // settuping cells
        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.rows, grid.columns];
            int cellSize = 25;
            for (int r = 0; r < grid.rows; r++)
            {
                for (int c = 0; c < grid.columns; c++)
                {
                    Image imagecontrol = new Image
                    {
                        Width = cellSize,
                        Height = cellSize,
                    };

                    Canvas.SetTop(imagecontrol, (r - 2) * cellSize + 10);
                    Canvas.SetLeft(imagecontrol, c * cellSize);
                    GameCanvas.Children.Add(imagecontrol);
                    imageControls[r, c] = imagecontrol;
                }
            }

            return imageControls;
        }

        // drawing game grid
        private void DrawGrid(GameGrid grid)
        {
            for (int r = 0; r < grid.rows; r++)
            {
                for (int c = 0; c < grid.columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Opacity = 1;
                    imageControls[r, c].Source = tiles[id];
                }
            }
        }

        // drawing current tetramino
        private void DrawMino(Tetramino mino)
        {
            foreach (Position p in mino.TilePositions())
            {
                imageControls[p.row, p.column].Opacity = 1;
                imageControls[p.row, p.column].Source = tiles[mino.id];
            }
        }

        // drawing next tetramino in image block
        private void DrawNextMino(Tetramino_Queue minoQueue)
        {
            Tetramino nextMino = minoQueue.NextMino;
            NextImage.Source = tetraminoImages[nextMino.id];
        }

        // drawing holding tetramino in image block
        private void DrawHeldMino(Tetramino heldMino)
        {
            if (heldMino == null) HoldImage.Source = tetraminoImages[0];
            else HoldImage.Source = tetraminoImages[heldMino.id];
        }

        // drawing ghost tetramino
        private void DrawGhostMino(Tetramino mino)
        {
            int dropDistance = game.DropMinoDistance();

            foreach (Position p in mino.TilePositions())
            {
                imageControls[p.row + dropDistance, p.column].Opacity = 0.25;
                imageControls[p.row + dropDistance, p.column].Source = tiles[mino.id];
            }
        }
        
        // drawing all we need for game: greed, score, previous, current, next and ghost tetraminos
        private void Draw(GameState game)
        {
            DrawGrid(game.grid);
            DrawGhostMino(game.CurrentMino);
            DrawMino(game.CurrentMino);
            DrawNextMino(game.MinoQueue);
            ScoreText.Text = $"Score: {game.Score}";
            DrawHeldMino(game.HeldMino);
        }

        private async Task GameLoop()
        {
            Draw(game);

            while(!game.game_over)
            {
                int delay = Math.Max(minDelay, maxDelay - (game.Score * delayDecrease));
                await Task.Delay(delay);
                game.MoveMinoDown();
                Draw(game);
            }

            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {game.Score}";
        }
        
        // cheching pressed button
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (game.game_over) return;

            switch (e.Key)
            {
                case Key.A: 
                    game.MoveMinoLeft();
                    break;

                case Key.D:
                    game.MoveMinoRight();
                    break;

                case Key.S:
                    game.MoveMinoDown();
                    break;

                case Key.R:
                    game.RotateMino();
                    break;

                case Key.Q:
                    game.RotateMinoCounter();
                    break;

                case Key.C:
                    game.HoldMino();
                    break;

                case Key.Space:
                    game.DropMino();
                    break;

                default: return;
            }

            Draw(game);
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }

        // after loosing - show game over, score and button for restarting game
        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            game = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }
    }
}
