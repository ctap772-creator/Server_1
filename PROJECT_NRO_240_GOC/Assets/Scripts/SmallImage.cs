using System;
using System.Collections.Generic;
using Assets.src.e;

public class SmallImage
{
	public static int[][] smallImg;

	public static SmallImage instance;

	public static Image[] imgbig;

	public static Small[] imgNew;

	public static MyVector vKeys = new MyVector();

	public static Image imgEmpty = null;

	public static sbyte[] newSmallVersion;

	public static int smallCount;

	public static short maxSmall;

	public static Dictionary<int, Image> imageRaw = new Dictionary<int, Image>();

	public SmallImage()
	{
		readImage();
	}

	public static void loadBigRMS()
	{
		if (imgbig == null)
		{
			imgbig = new Image[5]
			{
				GameCanvas.loadImageRMS("/img/Big0.png"),
				GameCanvas.loadImageRMS("/img/Big1.png"),
				GameCanvas.loadImageRMS("/img/Big2.png"),
				GameCanvas.loadImageRMS("/img/Big3.png"),
				GameCanvas.loadImageRMS("/img/Big4.png")
			};
		}
	}

	public static void loadBigImage()
	{
		imgEmpty = Image.createRGBImage(new int[1], 1, 1, bl: true);
	}

	public static void init()
	{
		instance = null;
		instance = new SmallImage();
	}

	public void readImage()
	{
		int num = 0;
		try
		{
			sbyte[] array = Rms.loadRMS("NR_image");
			if (array == null)
			{
				return;
			}
			DataInputStream dataInputStream = new DataInputStream(array);
			short num2 = dataInputStream.readShort();
			smallImg = new int[num2][];
			for (int i = 0; i < smallImg.Length; i++)
			{
				smallImg[i] = new int[5];
			}
			for (int j = 0; j < num2; j++)
			{
				num++;
				smallImg[j][0] = dataInputStream.readUnsignedByte();
				smallImg[j][1] = dataInputStream.readShort();
				smallImg[j][2] = dataInputStream.readShort();
				smallImg[j][3] = dataInputStream.readShort();
				smallImg[j][4] = dataInputStream.readShort();
			}
			int newSize = System.Math.Max((int)num2, 30000);
			imgNew = new Small[newSize];
			maxSmall = (short)newSize;
			newSmallVersion = new sbyte[newSize];
		}
		catch (Exception ex)
		{
			Cout.LogError3("Loi readImage: " + ex.ToString() + "i= " + num);
		}
	}

	public static void clearHastable()
	{
	}

	public static bool isValidIconId(int id)
	{
		return imgNew != null && id >= 0 && id < imgNew.Length;
	}

	public static Small getSmallSafe(int id)
	{
		if (!isValidIconId(id))
		{
			return null;
		}
		return imgNew[id];
	}

	public static void paintStandAuraFrame(mGraphics g, int iconId, int frameCount, int x, int y)
	{
		if (!isValidIconId(iconId))
		{
			return;
		}
		Small small = imgNew[iconId];
		if (small == null)
		{
			createImage(iconId);
			return;
		}
		if (small.img == null)
		{
			return;
		}
		if (frameCount < 1)
		{
			frameCount = 1;
		}
		int frameH = mGraphics.getImageHeight(small.img) / frameCount;
		int frameW = mGraphics.getImageWidth(small.img);
		if (frameH <= 0 || frameW <= 0)
		{
			return;
		}
		int y0 = GameCanvas.gameTick / 4 % frameCount * frameH;
		g.drawRegion(small.img, 0, y0, frameW, frameH, 0, x, y, mGraphics.BOTTOM | mGraphics.HCENTER);
	}

	public static void createImage(int id)
	{
		if (imgNew == null || id < 0 || id >= imgNew.Length)
		{
			return;
		}
		if (mGraphics.zoomLevel == 1)
		{
			Image image = GameCanvas.loadImage("/SmallImage/Small" + id + ".png");
			if (image != null)
			{
				imgNew[id] = new Small(image, id);
				return;
			}
			imgNew[id] = new Small(imgEmpty, id);
			Service.gI().requestIcon(id);
			return;
		}
		Image image2 = GameCanvas.loadImage("/SmallImage/Small" + id + ".png");
		if (image2 != null)
		{
			imgNew[id] = new Small(image2, id);
			return;
		}
		bool flag = false;
		if (imageRaw.ContainsKey(id))
		{
			Image img = null;
			imageRaw.TryGetValue(id, out img);
			if (img != null)
			{
				imgNew[id] = new Small(img, id);
			}
			else
			{
				flag = true;
			}
		}
		else
		{
			flag = true;
		}
		if (flag)
		{
			imgNew[id] = new Small(imgEmpty, id);
			Service.gI().requestIcon(id);
		}
	}

	public static void drawSmallImage(mGraphics g, int id, int x, int y, int transform, int anchor)
	{
		if (!isValidIconId(id))
		{
			return;
		}
		if (imgbig == null)
		{
			Small small = imgNew[id];
			if (small == null)
			{
				createImage(id);
			}
			else if (small.img != null)
			{
				g.drawRegion(small.img, 0, 0, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img), transform, x, y, anchor);
			}
		}
		else if (smallImg != null)
		{
			if (id >= smallImg.Length || smallImg[id] == null || smallImg[id][1] >= 256 || smallImg[id][3] >= 256 || smallImg[id][2] >= 256 || smallImg[id][4] >= 256)
			{
				Small small2 = imgNew[id];
				if (small2 == null)
				{
					createImage(id);
				}
				else
				{
					small2.paint(g, transform, x, y, anchor);
				}
			}
			else
			{
				int num = smallImg[id][0];
				if (num >= 0 && num < imgbig.Length && imgbig[num] != null)
				{
					g.drawRegion(imgbig[num], smallImg[id][1], smallImg[id][2], smallImg[id][3], smallImg[id][4], transform, x, y, anchor);
				}
			}
		}
		else if (GameCanvas.currentScreen != GameScr.gI())
		{
			Small small3 = imgNew[id];
			if (small3 == null)
			{
				createImage(id);
			}
			else
			{
				small3.paint(g, transform, x, y, anchor);
			}
		}
	}

	public static void drawSmallImage(mGraphics g, int id, int f, int x, int y, int w, int h, int transform, int anchor)
	{
		if (!isValidIconId(id))
		{
			return;
		}
		if (imgbig == null)
		{
			Small small = imgNew[id];
			if (small == null)
			{
				createImage(id);
			}
			else if (small.img != null)
			{
				int y0 = f * w;
				if (mGraphics.getImageHeight(small.img) <= h)
				{
					y0 = 0;
				}
				g.drawRegion(small.img, 0, y0, w, h, transform, x, y, anchor);
			}
		}
		else if (smallImg != null)
		{
			if (id >= smallImg.Length || smallImg[id] == null || smallImg[id][1] >= 256 || smallImg[id][3] >= 256 || smallImg[id][2] >= 256 || smallImg[id][4] >= 256)
			{
				Small small2 = imgNew[id];
				if (small2 == null)
				{
					createImage(id);
				}
				else
				{
					small2.paint(g, transform, f, x, y, w, h, anchor);
				}
			}
			else
			{
				int num = smallImg[id][0];
				if (num != 4 && num >= 0 && num < imgbig.Length && imgbig[num] != null)
				{
					int y0 = f * w;
					if (mGraphics.getImageHeight(imgbig[num]) <= h)
					{
						y0 = 0;
					}
					g.drawRegion(imgbig[num], 0, y0, w, h, transform, x, y, anchor);
				}
				else
				{
					Small small3 = imgNew[id];
					if (small3 == null)
					{
						createImage(id);
					}
					else
					{
						small3.paint(g, transform, f, x, y, w, h, anchor);
					}
				}
			}
		}
		else if (GameCanvas.currentScreen != GameScr.gI())
		{
			Small small4 = imgNew[id];
			if (small4 == null)
			{
				createImage(id);
			}
			else
			{
				small4.paint(g, transform, f, x, y, w, h, anchor);
			}
		}
	}

	public static void update()
	{
		int num = 0;
		if (GameCanvas.gameTick % 1000 != 0 || imgNew == null)
		{
			return;
		}
		for (int i = 0; i < imgNew.Length; i++)
		{
			if (imgNew[i] != null)
			{
				num++;
				imgNew[i].update();
				smallCount++;
			}
		}
		if (num > 200 && GameCanvas.lowGraphic)
		{
			imgNew = new Small[maxSmall];
		}
	}
}
