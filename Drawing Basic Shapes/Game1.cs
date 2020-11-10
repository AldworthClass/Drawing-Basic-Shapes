using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Drawing_Basic_Shapes
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Textures

        Rectangle fillRect, borderRect;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            fillRect = new Rectangle(10, 10, 150, 300);
            borderRect = new Rectangle(500, 10, 150, 200);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(makeRectFill(Color.AntiqueWhite), fillRect, Color.White);
            _spriteBatch.Draw(makeRectOutline(borderRect, 2, Color.AntiqueWhite), borderRect, Color.White);


            _spriteBatch.End();


            base.Draw(gameTime);
        }



        //  Taken from https://stackoverflow.com/questions/5751732/draw-rectangle-in-xna-using-spritebatch

        public Texture2D makeRectFill(Color color)
        {
            Texture2D rect = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            rect.SetData(new[] { color });          
            return rect;
        }

        // Taken from https://stackoverflow.com/questions/13893959/how-to-draw-the-border-of-a-square/13894276

        public Texture2D makeRectOutline(Rectangle size, int borderWidth, Color color)
        {
            Texture2D rect = new Texture2D(_graphics.GraphicsDevice, size.Width, size.Height);

            Color[] colors = new Color[size.Width * size.Height];

            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    bool colored = false;
                    for (int i = 0; i <= borderWidth; i++)
                    {
                        if (x == i || y == i || x == size.Width - 1 - i || y == size.Height - 1 - i)
                        {
                            colors[x + y * size.Width] = color;
                            colored = true;
                            break;
                        }
                    }

                    if (colored == false)
                        colors[x + y * size.Width] = Color.Transparent;
                }
            }
            rect.SetData(colors);
            return rect;
        }

    }
}
