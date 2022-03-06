using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EntitledEngine.EntitledEngine
{
	public class Sprite2D
	{
		public Vector2 Position = null;
		public Vector2 Scale = null;
		public string Directory = "";
		public string Tag = "";
		public Image Sprite = null;
		public bool IsRefference = false;

		public Sprite2D(Vector2 Position, Vector2 Scale, string Directory, string Tag)
		{
			this.Position = Position;
			this.Scale = Scale;
			this.Directory = Directory;
			this.Tag = Tag;

			Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");
			Bitmap sprite = new Bitmap(tmp,(int)this.Scale.X, (int)this.Scale.Y);

			Sprite = sprite;

			Log.Info($"[SHAPE2D]({Tag}) - has been registerd");
			EntitledEngine.RegisterSprite(this);
		}
		public Sprite2D(bool IsReference, string Directory)
		{
			this.IsRefference = IsReference;
			this.Directory = Directory;

			Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");

			Bitmap sprite = new Bitmap(tmp, (int)this.Scale.X, (int)this.Scale.Y);
			Sprite = sprite;

			Log.Info($"[SHAPE2D]({Tag}) - has been registerd");
			EntitledEngine.RegisterSprite(this);
		}
		public Sprite2D(Vector2 Position, Vector2 Scale, Sprite2D refference, string Tag)
		{
			this.Position = Position;
			this.Scale = Scale;
			this.Tag = Tag;

			Sprite = refference.Sprite;

			Log.Info($"[SHAPE2D]({Tag}) - has been registerd");
			EntitledEngine.RegisterSprite(this);
		}
		public void DestroySelf()
		{
			EntitledEngine.UnRegisterSprite(this);
		}
	}
}
