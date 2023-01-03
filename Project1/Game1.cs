using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static TiledMap _tiledMap;
        public static TiledMapRenderer _tiledMapRenderer;

        public RenderTarget2D _renderTarget;

        public int largeurFenetre = 1080;
        public int hauteurFenetre = 720;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Créez une nouvelle caméra
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _graphics.PreferredBackBufferWidth = largeurFenetre;
            _graphics.PreferredBackBufferHeight = hauteurFenetre;
            _graphics.ApplyChanges();
            PresentationParameters pp = _graphics.GraphicsDevice.PresentationParameters; 
            _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice, 320, 240, false,
                SurfaceFormat.Color, DepthFormat.None, pp.MultiSampleCount, RenderTargetUsage.DiscardContents);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("Map/map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            _tiledMapRenderer.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.SetRenderTarget(_renderTarget); 
            GraphicsDevice.Clear(Color.Transparent); 
            GraphicsDevice.BlendState = BlendState.AlphaBlend; 
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp; 
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            _tiledMapRenderer.Draw();

            GraphicsDevice.SetRenderTarget(null); 
            GraphicsDevice.Clear(Color.Red); //I do this to have a background color

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_renderTarget, destinationRectangle: new Rectangle(0, 0, largeurFenetre, hauteurFenetre), color: Color.White); 
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}