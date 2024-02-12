using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FiniteStateMachine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Restaurant _restaurant;

        private Texture2D man;
        private Texture2D menu;
        private Texture2D price;
        private Texture2D restaurant;
        private Texture2D salad;
        private Texture2D table;
        private Texture2D next_button;
        private Texture2D prev_button;

        private Button next;
        private Button prev;

        private MouseState lastMouseState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.PreferredBackBufferWidth = 1500;
            _graphics.ApplyChanges();
            _restaurant = new Restaurant();
            next = new Button(new Rectangle(1200, 800, 200, 100));
            prev = new Button(new Rectangle(980, 800, 200, 100));
            next.Enable();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            man = Content.Load<Texture2D>("man");
            menu = Content.Load<Texture2D>("menu");
            price = Content.Load<Texture2D>("price");
            restaurant = Content.Load<Texture2D>("restaurant");
            salad = Content.Load<Texture2D>("salad-image");
            table = Content.Load<Texture2D>("table");
            next_button = Content.Load<Texture2D>("next");
            prev_button = Content.Load<Texture2D>("prev");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            next.UpdateTimer();
            prev.UpdateTimer();

            lastMouseState = Mouse.GetState();

            next.CheckPressed(lastMouseState.X, lastMouseState.Y, lastMouseState.LeftButton == ButtonState.Pressed);
            if(prev.IsEnabled()) prev.CheckPressed(lastMouseState.X, lastMouseState.Y, lastMouseState.LeftButton == ButtonState.Pressed);

            if (next.IsPressed())
            {
                _restaurant.NextState();
                switch (_restaurant.GetState())
                {
                    case Restaurant.States.food:
                        prev.Enable();
                        break;
                    case Restaurant.States.exit:
                        Exit();
                        break;
                    default:
                        prev.Disable();
                        break;
                }
            }
            if (prev.IsPressed())
            {
                _restaurant.PrevState();
                Restaurant.States st = _restaurant.GetState();
                prev.Disable();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            switch(_restaurant.GetState())
            {
                case Restaurant.States.enter:
                    _spriteBatch.Draw(restaurant, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                    _spriteBatch.Draw(man, new Rectangle(150, 350, 433, 650), Color.White);
                    break;
                case Restaurant.States.sit:
                    _spriteBatch.Draw(restaurant, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                    _spriteBatch.Draw(table, new Rectangle(150, 800, 1200, 900), Color.White);
                    break;
                case Restaurant.States.menu:
                    _spriteBatch.Draw(menu, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                    break;
                case Restaurant.States.food:
                    _spriteBatch.Draw(restaurant, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                    _spriteBatch.Draw(table, new Rectangle(150, 800, 1200, 900), Color.White);
                    _spriteBatch.Draw(salad, new Rectangle(400, 600, 700, 400), Color.White);
                    break;
                case Restaurant.States.pay:
                    _spriteBatch.Draw(price, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                    break;
                case Restaurant.States.leave:
                    _spriteBatch.Draw(restaurant, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                    _spriteBatch.Draw(man, new Rectangle(700, 350, 433, 650), Color.White);
                    break;
            }
            if (next.IsEnabled())
            {
                _spriteBatch.Draw(next_button, next.GetPosition(), Color.White);
            }
            if (prev.IsEnabled())
            {
                _spriteBatch.Draw(prev_button, prev.GetPosition(), Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}