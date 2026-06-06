namespace Assets.src.e
{
	public class Small
	{
		public Image img;

		public int id;

		public int timePaint;

		public int timeUpdate;

		public Small(Image img, int id)
		{
			this.img = img;
			this.id = id;
			timePaint = 0;
			timeUpdate = 0;
		}

		public void paint(mGraphics g, int transform, int x, int y, int anchor)
		{
			if (img != null)
			{
				g.drawRegion(img, 0, 0, mGraphics.getImageWidth(img), mGraphics.getImageHeight(img), transform, x, y, anchor);
				if (GameCanvas.gameTick % 1000 == 0)
				{
					timePaint++;
					timeUpdate = timePaint;
				}
			}
		}

		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor)
		{
			paint(g, transform, f, x, y, w, h, anchor, isClip: false);
		}

		public void paint(mGraphics g, int transform, int f, int x, int y, int w, int h, int anchor, bool isClip)
		{
			if (img != null && mGraphics.getImageWidth(img) != 1)
			{
				int y0 = f * w;
				if (mGraphics.getImageHeight(img) <= h)
				{
					y0 = 0;
				}
				g.drawRegion(img, 0, y0, w, h, transform, x, y, anchor, isClip);
				if (GameCanvas.gameTick % 1000 == 0)
				{
					timePaint++;
					timeUpdate = timePaint;
				}
			}
		}

		public void update()
		{
		}
	}
}
