using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Drawing_Basic_Shapes
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Textures

        Rectangle fillRect, borderRect, ellpiseRect;


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
            ellpiseRect = new Rectangle(200, 10, 100, 50);

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
            _spriteBatch.Draw(makeEllipse(ellpiseRect, Color.Red), ellpiseRect, Color.White);
            DrawLine(_spriteBatch, new Vector2(0, 0), new Vector2(200, 200), 4);

            _spriteBatch.End();


            base.Draw(gameTime);
        }

        // Taken from https://stackoverflow.com/questions/2519304/draw-simple-circle-in-xna
        public Texture2D makeEllipse(Rectangle rect, Color color)
        {
            int radius;
            if (rect.Width >= rect.Height)
                radius = rect.Width;
            else
                radius = rect.Height;

            Texture2D texture = new Texture2D(GraphicsDevice, radius, radius);
            Color[] colorData = new Color[radius * radius];

            float diam = radius / 2f;
            float diamsq = diam * diam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = color;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }
            texture.SetData(colorData);
            return texture;
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

        void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, int thickness)
        {
            Vector2 edge = end - start;
            Texture2D t = new Texture2D(GraphicsDevice, 1, 1);
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(t,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    thickness), //width of line, change this to make thicker line
                null,
                Color.Red, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }

    }
}
