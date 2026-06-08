using System;
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mod;
using Mod.XMAP;
using UnityEngine;
using UnityEngine.Networking;
using Assets.src.g;

public class ModFunc : IActionListener
{
	public class Point
	{
		public int x;

		public int y;

		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}

	private static readonly ModFunc Instance = new ModFunc();

	public static string homeUrl = "Dragonboy";

	public static bool ModNotLogo = false;

	public static bool ModNotLogoGif = false;

	public static bool isReadInt = true;//đag đọc int

	public static bool isReadDouble = false;//true = đọc double

	public static bool isActiveTamBao = false;

	public static bool isActiveNapTuan = false;

	public static bool isEncryptIcon = false;

	public static bool isVietnamese = false;

	public static bool isShowMenuChat = false;

	public static bool isMenuVisible = false;

	public static float arrowRotation = 0f;

	public static float menuX = 0f;

	public static float targetMenuX = 0f;

	public static float targetArrowRotation = 0f;

	public static float ANIMATION_SPEED = 0.1f;

	private static bool isDebugEnable = false;

	private static long lastTimeLog = 0L;

	public bool canUpdate;

	public static Command cmdAccManager;

	public static bool isOpenAccMAnager = false;

	public static List<Account> accounts = new List<Account>();

	public List<Command> cmdsChooseAcc = new List<Command>();

	public List<Command> cmdsDelAcc = new List<Command>();

	public static Command cmdCloseAccManager;

	private static int modKeyPosX;

	private static int modKeyPosY;

	public static bool isAutoLogin = false;

	public static bool dangLogin = false;

	public static AutoLogin autoLogin;

	public static bool isAutoNoitai = false;

	public bool autoAttack;

	public bool autoWakeUp;

	public long lastAutoWakeUp;

	public bool isAutoPhaLe;

	public int autoPhaLeBatchMode;

	public int pendingAutoSaleThoiVangIndex = -1;

	public bool isAutoNhapNgocRong;

	public bool isAutoDapDo;

	public long lastAutoNhapNgocRong;

	public long lastAutoDapDo;

	private long lastCombine1Time;

	public bool isAutoVQMM;

	public long lastVQMM;

	private long lastAutoNhapNgocRongNotice;

	private long lastAutoNhapNgocRongCombineRequest;

	private bool isWaitingAutoNhapNgocRongMenu;

	private int autoDapDoUpgradeCount;

	private bool isWaitingAutoDapDoMenu;

	private long lastAutoDapDoCombineRequest;

	private int autoDapDoTargetLevel = -1;

	private const int ActionToggleAutoNhapNgocRongMenu = 100301;

	private const int ActionStartAutoDapDo = 100302;

	private const short AutoDapDoNpcId = 21;

	private const int AutoDapDoBaseInterval = 100;

	private const int AutoDapDoMinInterval = 40;

	private const int AutoDapDoRequestTimeout = 12000;

	private const string AutoNhapNgocRongMenuCaption = "Auto Nhập";

	private const string StopAutoNhapNgocRongMenuCaption = "Dừng Auto";

	private const int AutoNhapNgocRongBaseInterval = 800;

	private const int AutoNhapNgocRongMinInterval = 200;

	private const int AutoNhapNgocRongRequestTimeout = 1500;

	private const int LowFpsTarget = 30;

	private const int HighFpsTarget = 60;

	private const int UltraFpsTarget = 120;

	private static readonly int[] AutoNhapNgocRongMapIds = new int[7] { 39, 40, 42, 43, 44, 84, 104 };

	private static readonly int[] PorataItemIds = new int[5] { 1884, 1255, 2104, 921, 454 };

	public static string strAutoNhapNgocRong = "T\u1EF1 \u0110\u1ED9ng Nh\u1EADp Ng\u1ECDc R\u1ED3ng";

	private long lastAutoAttack;

	private readonly List<Skill> listSkillsAuto = new List<Skill>();

	public List<ItemAuto> listItemAuto = new List<ItemAuto>();

	private static bool isAutoChat = false;

	private static string textAutoChat = string.Empty;

	private static bool isAutoChatTG = false;

	private static string textAutoChatTG = string.Empty;

	public static bool startAutoItem = false;

	private long lastAutoChat;

	private long lastAutoChatTG;

	public static bool isFilterItem = false;

	public static bool isAutoFilterItem = false;

	public static List<ItemAutoFilter> listFilterItems = new List<ItemAutoFilter>();

	public static bool isShowFilterList = false;

	private bool isResizing;

	private int lastMouseX;

	private int lastMouseY;

	private int lastPanelX;

	private int lastPanelY;

	private int lastPanelW;

	private int lastPanelH;

	private int lastScrollY;

	private bool isScrolling;

	private int scrollY;

	private readonly int MAX_ITEMS_VISIBLE = 10;

	private long lastFilterTime;

	public static bool notifBoss = true;

	public static bool notifKillBoss = true;

	private bool lineToBoss;

	private bool focusBoss;

	private long lastFocusBoss;

	public static MyVector activeBossNotif = new MyVector();

	public static MyVector killedBossNotif = new MyVector();

	public bool showCharsInMap = true;

	public bool userOpenZones;

	public bool isUpdateZones;

	private long lastUpdateZones;

	public MyVector charsInMap = new MyVector();

	public static int zoneMacDinh;

	public static bool isdoBoss;

	private static long currDoBoss;

	public static string bossCanDo;

	public Item itemPhale;

	public int maxPhale = -1;

	public int currPhale = -1;

	public bool isCollectAll;

	public bool isPaintThuongDe;

	public bool isOpenThuongDe;

	private static int currentPage = 0;

	private int ChiSoNoiTai = -1;

	public string curSelectIntrinsic = "";

	private string CurrentNoiTai = "";

	private long lastTimeUpdateNoiTai;

	private string currentPlayerNoiTai = "";

	private int CurrentParamNoitaiPlayer;

	public MyVector listNotifTichXanh = new MyVector();

	private bool startChat;

	private int xNotif;

	private long lastUpdateNotif;

	public bool isPeanPet;

	private long lastPeanPet;

	public static bool autoPointForPet = false;

	public static bool userOpenPet = false;

	public static int indexAutoPoint = -1;

	public static int pointIncrease = 0;

	public bool showInfoMe;

	private long lastUpdateInfoMe;

	public bool isShowButton = true;

	public bool isIntroOff;

	public static bool isInventory = true;

	public static bool isEffectInven = false;

	public static bool isLogo = false;

	public static bool isLogoGif = false;

	public static bool GiamDungLuong = false;

	public static bool AnPlayer = false;

	public bool isHighFps;

	public bool isFps120;

	public int fpsMode;

	public static bool isShowID = true;

	private static int FrameGif = 15;

	private static int FrameGifMenu = 16;

	public static Image[] ticks = new Image[20];

	private static Image logo = new Image();

	private static Image[] logos = new Image[FrameGif];

	private static Image[] logosMenu = new Image[FrameGifMenu];

	public static Image imgLogoBig = null;

	public static Image imgBg = null;

	public static bool isShortOptionTemp = false;

	public static Image imgMenuChat = null;

	public static Image imgCloseButton = null;

	public static Image imgNextPage = null;

	public static Image imgNextPage2 = null;

	public static Image imgPrevPage = null;

	public static Image imgPrevPage2 = null;

	public static int musicCount = 0;

	public static bool loadedMusic = false;

	public static bool isPlayingMusic = false;

	public static List<AudioClip> musics = new List<AudioClip>();

	private static string backgroundColor = "0 0 0";

	public static bool deVeNhaKhiTachHT;

	public static string strDeVeNhaKhiTachHT = "Cho \u0110\u1EC7 V\u1EC1 Nh\u00E0 Khi T\u00E1ch HT";

	public static bool isEditButton = false;

	public static int slThoiVangCanBan = 1;

	public static string strThoiVangBanMoiLan = "Th\u1ECFi V\u00E0ng B\u00E1n/L\u1EA7n";

	public static string strThoiVangBanMoiLanPrompt = "Nh\u1eadp " + strThoiVangBanMoiLan;

	private const string SlThoiVangCanBanRmsKey = "slThoiVangCanBan";

	private static Dictionary<string, Point> buttonPositions = new Dictionary<string, Point>();

	private static string selectedButton = null;

	private static Point dragStart = null;

	private static bool isDragging = false;

	public static string ipServer = "Đổi IP";

	public static bool isLockFocus = false;

	public static string strAddAutoItem = "Thêm vào\nAutoItem";

	public static string strRemoveAutoItem = "Xoá khỏi\nAutoItem";

	public static string strAddFilterItem = "Thêm vào\nDS lọc";

	public static string strRemoveFilterItem = "Xóa khỏi\nDS lọc";

	public static string strTeleportTo = "D\u1ECBch\nchuy\u1EC3n t\u1EDBi";

	public static string strChooseIntrinsic = "Ch\u1ECDn ch\u1EC9 s\u1ED1";

	public static string strInCrease = "T\u0103ng\nt\u1EDBi\nm\u1EE9c";

	public static string[] strPointTypes = new string[5] { "HP", "MP", "S\u1EE9c \u0110\u00E1nh", "Gi\u00E1p", "Ch\u00ED m\u1EA1ng" };

	public static string strAccManager = "Q.L.T.K";

	public static string strModFunc = "Ch\u1EE9c N\u0103ng MOD";

	public static string strUpdateZones = "C\u1EADp Nh\u1EADt Khu";

	public static string strCharsInMap = "Nh\u00E2n V\u1EADt Trong Khu";

	public static string strInfoMe = "Th\u00F4ng Tin B\u1EA3n Th\u00E2n";

	public static string strAutoPhaLe = "T\u1EF1 \u0110\u1ED9ng Pha L\u00EA H\u00F3a";

	public static string strAutoVQMM = "T\u1EF1 \u0110\u1ED9ng VQMM";

	public static string strAutoWakeUp = "T\u1EF1 \u0110\u1ED9ng H\u1ED3i Sinh";

	public static string strAutoLogin = "T\u1EF1 \u0110\u1ED9ng \u0110\u0103ng Nh\u1EADp";

	public static string strShowButton = "Hi\u1EC7n N\u00FAt Tr\u1EE3 N\u0103ng";

	public static string strIntroOff = "T\u1EAFt Intro";

	public static string strInventoryOFF = "Hi\u1EC7n H\u00E0nh Trang L\u01B0\u1EDBi";

	public static string strEffectOff = "T\u1EAFt Hi\u1EC7u \u1EE8ng H\u00E0nh Trang";

	public static string strHighFps = "FPS Cao";

	public static string strClickToChat = " [Ấn để chat]";

	public static string strPlayerInfo = "Thông tin player";

	public static string strPet2 = "Người iuu";

	public static string strUseForPet2 = "Sử dụng\ncho\nNg.iuu";

	public static string strLogo = "Ẩn / hiện Logo";

	public static string strGiamDungLuong = "Giảm Dung Lượng";

	public static string strAnPlayer = "Ẩn Player";

	public static string strLogoGif = "Logo động";

	public static string strShowID = "Hiện ID Item/NPC";

	public static string strEditButton = "Chỉnh sửa nút";

	public static string strVietnamese = "Gõ Tiếng Việt";

	public static string strShowMenuChat = "Thông Tin Lệnh Chat";

	private static readonly Dictionary<string, Point> defaultButtonPositions = new Dictionary<string, Point>
	{
		{
			"Capsule",
			new Point(20, -26)
		},
		{
			"Fusion",
			new Point(-21, 21)
		},
		{
			"Zone",
			new Point(-66, 62)
		},
		{
			"MapLeft",
			new Point(-106, 62)
		},
		{
			"MapCenter",
			new Point(-66, 21)
		},
		{
			"MapRight",
			new Point(-21, -26)
		}
	};

	private static int panelX = GameCanvas.w / 3 + 25;

	private static int panelY = 15;

	private static int panelW = 200;

	private static int panelH = 170;

	public static void InitButtonPositions()
	{
		if (buttonPositions.Count != 0)
		{
			return;
		}
		foreach (KeyValuePair<string, Point> kvp in defaultButtonPositions)
		{
			buttonPositions[kvp.Key] = new Point(kvp.Value.x, kvp.Value.y);
		}
	}

	public static ModFunc GI()
	{
		return Instance ?? new ModFunc();
	}

	public void OpenMenu()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command("Bản đồ", 883));
		myVector.addElement(new Command("Luyện tập", 45));
		myVector.addElement(new Command("Nhặt đồ", 89));
		myVector.addElement(new Command("Đệ tử", 16));
		myVector.addElement(new Command("BOSS", 32));
		myVector.addElement(new Command("Khác", 53));
		GameCanvas.menu.startAt(myVector, 4);
	}

	public static Color GetColor()
	{
		string[] array = backgroundColor.Split(' ');
		return new Color(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
	}

	public bool UpdateKey(int key)
	{
		switch (key)
		{
			case 97:
				MoveTo(Char.myCharz().cx - 100, Char.myCharz().cy);
				break;
			case 119:
				MoveTo(Char.myCharz().cx, Char.myCharz().cy - 100);
				break;
			case 115:
				MoveTo(Char.myCharz().cx, Char.myCharz().cy + 100);
				break;
			case 100:
				MoveTo(Char.myCharz().cx + 100, Char.myCharz().cy);
				break;
			case 101:
				Service.gI().friend(0, -1);
				InfoDlg.showWait();
				break;
			case 104:
				GameScr.gI().onChatFromMe("ukhu", string.Empty);
				break;
			case 117:
				perform(42, null);
				break;
			case 120:
				OpenMenu();
				break;
			case 102:
				UsePorata();
				break;
			case 99:
				UseItem(194);
				break;
			case 109:
				userOpenZones = true;
				Service.gI().openUIZone();
				break;
			case 116:
				UseItem(521);
				break;
			case 110:
				PickMob.IsAutoPickItems = !PickMob.IsAutoPickItems;
				GameScr.info1.addInfo("Tự động nhặt: " + (PickMob.IsAutoPickItems ? "Bật" : "Tắt"), 0);
				break;
			case 106:
				ManualXmap.GI().LoadMapLeft();
				break;
			case 107:
				ManualXmap.GI().LoadMapCenter();
				break;
			case 108:
				ManualXmap.GI().LoadMapRight();
				break;
			case 103:
				if (Char.myCharz().charFocus != null)
				{
					Service.gI().giaodich(0, Char.myCharz().charFocus.charID, -1, -1);
					GameScr.info1.addInfo("Đã gửi lại mời giao dịch đến " + Char.myCharz().charFocus.cName, 0);
				}
				break;
			default:
				return false;
		}
		return true;
	}

	public void LoadGame()
	{
		if (!loadedMusic)
		{
			InitMusic();
			loadedMusic = true;
		}
		Time.timeScale = 1.5f;
		listSkillsAuto.Clear();
		listItemAuto.Clear();
		LoadFpsMode();
		isInventory = Rms.loadRMSInt("inventory") == 1;
		isEffectInven = Rms.loadRMSInt("effectinven") == 1;
		GiamDungLuong = Rms.loadRMSInt("background") == 1;
		deVeNhaKhiTachHT = Rms.loadRMSInt("deVeNhaKhiTachHT") == 1;
		ApplyBlackCameraBackground();
		AnPlayer = Rms.loadRMSInt("anplayer") == 1;
		autoWakeUp = Rms.loadRMSInt("autoWakeUp") == 1;
		isShowID = Rms.loadRMSInt("showid") != 0;
		if (!ModNotLogo)
		{
			isLogo = Rms.loadRMSInt("logo") == 1;
			isLogoGif = Rms.loadRMSInt("logoGif") == 1;
			if (isLogo)
			{
				if (isLogoGif)
				{
					LoadLogoGif();
				}
				else
				{
					LoadLogoImages();
				}
			}
		}
		ChangeFPSTarget();
		if (autoWakeUp)
		{
			GameScr.info1.addInfo("Tự động hồi sinh [Bật]", 0);
		}
		LoadButtonPositions();
		LoadSlThoiVangCanBan();
		autoPhaLeBatchMode = Rms.loadRMSInt("autoPhaLeBatchMode");
		if (autoPhaLeBatchMode < 1 || autoPhaLeBatchMode > 3)
		{
			autoPhaLeBatchMode = 3;
		}
		isAutoPhaLe = false;
		QuaNapTuan.isNapTuan = false;
	}

	public void MoveTo(int x, int y)
	{
		Char.myCharz().cx = x;
		Char.myCharz().cy = y;
		Service.gI().charMove();
		if (!ItemTime.isExistItem(4387))
		{
			Char.myCharz().cx = x;
			Char.myCharz().cy = y + 1;
			Service.gI().charMove();
			Char.myCharz().cx = x;
			Char.myCharz().cy = y;
			Service.gI().charMove();
		}
	}

	public void GotoNpc(int npcID)
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			if (npc.template.npcTemplateId == npcID && Math.abs(npc.cx - Char.myCharz().cx) >= 50)
			{
				MoveTo(npc.cx, npc.cy - 1);
				Char.myCharz().FocusManualTo(npc);
				break;
			}
		}
	}

	public int FindItemIndex(int idItem)
	{
		if (Char.myCharz().arrItemBag == null)
		{
			return -1;
		}
		for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
		{
			if (Char.myCharz().arrItemBag[i] != null && Char.myCharz().arrItemBag[i].template.id == idItem)
			{
				return Char.myCharz().arrItemBag[i].indexUI;
			}
		}
		return -1;
	}

	private void AttackChar()
	{
		try
		{
			MyVector myVector = new MyVector();
			myVector.addElement(Char.myCharz().charFocus);
			Service.gI().sendPlayerAttack(new MyVector(), myVector, 2);
		}
		catch
		{
		}
	}

	public void AttackMob(Mob mob)
	{
		try
		{
			MyVector myVector = new MyVector();
			myVector.addElement(mob);
			Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
		}
		catch
		{
		}
	}

	public void AutoAttack()
	{
		Char @char = Char.myCharz();
		if (!Char.isLoadingMap && !@char.stone && !@char.meDead && @char.statusMe != 14 && @char.statusMe != 5 && @char.myskill.template.type == 1 && @char.myskill.template.id != 10 && @char.myskill.template.id != 11 && !@char.myskill.paintCanNotUseSkill && mSystem.currentTimeMillis() - lastAutoAttack > 500)
		{
			if (GameScr.gI().isMeCanAttackMob(@char.mobFocus) && Res.abs(@char.mobFocus.xFirst - @char.cx) < @char.myskill.dx * 2)
			{
				AttackMob(@char.mobFocus);
				SetUsedSkill(@char.myskill);
			}
			else if (@char.isMeCanAttackOtherPlayer(@char.charFocus) && Res.abs(@char.charFocus.cx - @char.cx) < @char.myskill.dx * 2)
			{
				AttackChar();
				SetUsedSkill(@char.myskill);
			}
			lastAutoAttack = mSystem.currentTimeMillis();
		}
	}

	public void SetUsedSkill(Skill skill)
	{
		skill.paintCanNotUseSkill = true;
		skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
	}

	private Item FindPorataInBag()
	{
		if (Char.myCharz().arrItemBag == null)
		{
			return null;
		}
		for (int i = 0; i < PorataItemIds.Length; i++)
		{
			Item item = FindItemBagWithIndexUI(FindItemIndex(PorataItemIds[i]));
			if (item != null)
			{
				return item;
			}
		}
		return null;
	}

	private static bool HasPorataStats(Item item)
	{
		if (item?.itemOption == null)
		{
			return false;
		}
		for (int i = 0; i < item.itemOption.Length; i++)
		{
			ItemOption option = item.itemOption[i];
			if (option != null && option.IsValidOption())
			{
				return true;
			}
		}
		return false;
	}

	private static sbyte GetPorataBagIndex(Item porata)
	{
		if (porata == null || Char.myCharz().arrItemBag == null)
		{
			return -1;
		}
		if (porata.indexUI >= 0 && porata.indexUI < Char.myCharz().arrItemBag.Length && Char.myCharz().arrItemBag[porata.indexUI] == porata)
		{
			return (sbyte)porata.indexUI;
		}
		for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
		{
			if (Char.myCharz().arrItemBag[i] == porata)
			{
				return (sbyte)i;
			}
		}
		return -1;
	}

	private static void UsePorataFromBag(Item porata)
	{
		sbyte bagIndex = GetPorataBagIndex(porata);
		if (porata.template.id == 1884 && HasPorataStats(porata))
		{
			if (bagIndex >= 0)
			{
				Service.gI().useItem(0, 1, bagIndex, -1);
			}
			else
			{
				Service.gI().useItem(0, 1, -1, (short)porata.template.id);
			}
			return;
		}
		if (bagIndex >= 0)
		{
			Service.gI().useItem(0, 1, bagIndex, -1);
			return;
		}
		Service.gI().useItem(0, 1, -1, (short)porata.template.id);
	}

	public void UsePorata()
	{
		Item porata = FindPorataInBag();
		if (porata == null)
		{
			GameScr.info1.addInfo("Bạn không có bông tai", 0);
			return;
		}
		bool wasFused = Char.myCharz().isNhapThe;
		UsePorataFromBag(porata);
		if (wasFused)
		{
			if (deVeNhaKhiTachHT)
			{
				Service.gI().petStatus(3);
			}
		}
	}

	public void AutoFocusBoss()
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			Char @char = (Char)GameScr.vCharInMap.elementAt(i);
			if (@char != null && @char.charID < 0 && @char.cTypePk == 5 && !@char.cName.StartsWith("Đ"))
			{
				Char.myCharz().FocusManualTo(@char);
				break;
			}
		}
	}

	public int GetMapID(string mapName)
	{
		int result = -1;
		for (int i = 0; i < XmapController.mapNames.Length; i++)
		{
			if (XmapController.mapNames[i].Trim().ToLower().Equals(mapName.Trim().ToLower()))
			{
				result = i;
			}
		}
		return result;
	}

	private string CharGender(Char @char)
	{
		if (@char.cTypePk == 5)
		{
			return "BOSS";
		}
		if (@char.cgender == 0)
		{
			return "TĐ";
		}
		if (@char.cgender == 1)
		{
			return "NM";
		}
		if (@char.cgender == 2)
		{
			return "XD";
		}
		return "";
	}

	public void UseItem(int itemId)
	{
		int index = FindItemIndex(itemId);
		if (index != -1)
		{
			Service.gI().useItem(0, 1, (sbyte)index, -1);
		}
		else
		{
			GameScr.info1.addInfo("Không tìm thấy vật phẩm", 0);
		}
	}

	public void UseItemAuto()
	{
		if (listItemAuto.Count <= 0 || !startAutoItem)
		{
			if (!startAutoItem)
			{
				System.Threading.Tasks.Task.Delay(200).ContinueWith((System.Threading.Tasks.Task t) => startAutoItem = true);
			}
			return;
		}
		if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
		{
			listItemAuto.Clear();
			GameScr.info1.addInfo("Đã dừng auto sử dụng Item", 0);
			return;
		}
		for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
		{
			Item item = Char.myCharz().arrItemBag[i];
			if (item == null)
			{
				continue;
			}
			foreach (ItemAuto itemAuto in listItemAuto)
			{
				if (item.template.iconID == itemAuto.iconID && item.template.id == itemAuto.id && !ItemTime.isExistItem(item.template.iconID))
				{
					Service.gI().useItem(0, 1, (sbyte)FindItemIndex(item.template.id), -1);
					if (listItemAuto.Count == 1)
					{
						return;
					}
					break;
				}
			}
		}
	}

	private void AutoHoiSinh()
	{
		if (Char.myCharz().cHP <= 0.0 || Char.myCharz().meDead || Char.myCharz().statusMe == 14)
		{
			Service.gI().wakeUpFromDead();
		}
	}

	public static int GetCurrPhaLe(Item item)
	{
		for (int i = 0; i < item.itemOption.Length; i++)
		{
			if (item.itemOption[i].optionTemplate.id == 107)
			{
				return item.itemOption[i].param;
			}
		}
		return 0;
	}

	public static int GetItemUpgradeLevel(Item item)
	{
		if (item?.itemOption == null)
		{
			return 0;
		}
		for (int i = 0; i < item.itemOption.Length; i++)
		{
			if (item.itemOption[i].optionTemplate.id == 72)
			{
				return item.itemOption[i].param;
			}
		}
		return 0;
	}

	public static bool ShouldShowAutoDapDoButton(Panel panel)
	{
		return IsDapDoUpgradePanel(panel);
	}

	public static Item GetDapDoUpgradeItem(Panel panel)
	{
		if (panel?.vItemCombine == null)
		{
			return null;
		}
		Item fallbackItem = null;
		for (int i = 0; i < panel.vItemCombine.size(); i++)
		{
			Item item = (Item)panel.vItemCombine.elementAt(i);
			if (item?.template == null)
			{
				continue;
			}
			if (!HasUpgradeOption(item) && !item.isTypeBody())
			{
				continue;
			}
			Item resolvedItem = FindItemBagWithIndexUI(item.indexUI) ?? item;
			if (item.isTypeBody())
			{
				return resolvedItem;
			}
			if (fallbackItem == null)
			{
				fallbackItem = resolvedItem;
			}
		}
		return fallbackItem;
	}

	private static bool HasUpgradeOption(Item item)
	{
		if (item?.itemOption == null)
		{
			return false;
		}
		for (int i = 0; i < item.itemOption.Length; i++)
		{
			if (item.itemOption[i].optionTemplate.id == 72)
			{
				return true;
			}
		}
		return false;
	}

	public static bool IsDapDoUpgradePanel(Panel panel)
	{
		if (panel == null || panel.type != 12 || IsNhapNgocRongPanel(panel))
		{
			return false;
		}
		if (panel.vItemCombine == null || panel.vItemCombine.size() < 2)
		{
			return false;
		}
		return GetDapDoUpgradeItem(panel) != null;
	}

	public void AutoPhaLe()
	{
		while (isAutoPhaLe)
		{
			if (TileMap.mapID != 5)
			{
				GameScr.info1.addInfo("Cần đến Đảo Kame để sử dụng Tự động Pha lê hóa", 0);
				Thread.Sleep(500);
				break;
			}
			if (currPhale >= maxPhale && itemPhale != null && currPhale >= 0 && maxPhale > 0)
			{
				Sound.start(1f, Sound.l1);
				GameScr.info1.addInfo("Đã đạt đến số sao yêu cầu", 0);
				StopAutoPhaLe();
				break;
			}
			if (Char.myCharz().checkLuong() < 1)
			{
				GameScr.info1.addInfo("Không đủ ngọc để pha lê hóa, đã tắt Tự động", 0);
				StopAutoPhaLe();
				break;
			}
			if (Char.myCharz().xu > 10000000000L)
			{
				GotoNpc(21);
				if (itemPhale != null && maxPhale > 0)
				{
					while (!GameCanvas.menu.showMenu)
					{
						Service.gI().combine(1, GameCanvas.panel.vItemCombine);
						Thread.Sleep(100);
					}
					Service.gI().confirmMenu(21, (sbyte)GetAutoPhaLeConfirmMenuIndex());
					GameCanvas.menu.doCloseMenu();
					GameCanvas.panel.currItem = null;
					GameCanvas.panel.chatTField.isShow = false;
				}
			}
			else if (itemPhale != null)
			{
				BanVang();
			}
			Thread.Sleep(500);
		}
	}

	private void SetAutoNhapNgocRong(bool enabled)
	{
		isAutoNhapNgocRong = enabled;
		lastAutoNhapNgocRong = 0L;
		lastAutoNhapNgocRongNotice = 0L;
		lastAutoNhapNgocRongCombineRequest = 0L;
		isWaitingAutoNhapNgocRongMenu = false;
	}

	public void ToggleAutoNhapNgocRong()
	{
		SetAutoNhapNgocRong(!isAutoNhapNgocRong);
	}

	private void StopAutoNhapNgocRong(string message)
	{
		SetAutoNhapNgocRong(false);
		if (!string.IsNullOrEmpty(message))
		{
			GameScr.info1.addInfo(message, 0);
		}
	}

	private void NotifyAutoNhapNgocRong(string message, long currentTime)
	{
		if (string.IsNullOrEmpty(message) || currentTime - lastAutoNhapNgocRongNotice < 2000)
		{
			return;
		}
		GameScr.info1.addInfo(message, 0);
		lastAutoNhapNgocRongNotice = currentTime;
	}

	private int GetAutoNhapNgocRongInterval()
	{
		float num = Mathf.Max(1f, Time.timeScale);
		return Mathf.Max(AutoNhapNgocRongMinInterval, Mathf.RoundToInt((float)AutoNhapNgocRongBaseInterval / num));
	}

	private static string NormalizeVietnameseText(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder();
		string text2 = text.Replace("\r", " ").Replace("\n", " ").Normalize(NormalizationForm.FormD);
		for (int i = 0; i < text2.Length; i++)
		{
			char c = text2[i];
			if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark)
			{
				continue;
			}
			if (c == '\u0111' || c == '\u0110')
			{
				c = 'd';
			}
			stringBuilder.Append(char.ToLowerInvariant(c));
		}
		return stringBuilder.ToString();
	}

	private static bool ContainsNormalizedText(string text, string keyword)
	{
		return !string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(keyword) && NormalizeVietnameseText(text).Contains(NormalizeVietnameseText(keyword));
	}

	private static bool ContainsNormalizedText(string[] texts, string keyword)
	{
		if (texts == null || string.IsNullOrEmpty(keyword))
		{
			return false;
		}
		string text = NormalizeVietnameseText(keyword);
		for (int i = 0; i < texts.Length; i++)
		{
			if (NormalizeVietnameseText(texts[i]).Contains(text))
			{
				return true;
			}
		}
		return false;
	}

	private static int FindMenuIndexByCaption(string caption)
	{
		if (GameCanvas.menu == null || GameCanvas.menu.menuItems == null)
		{
			return -1;
		}
		string text = NormalizeVietnameseText(caption);
		for (int i = 0; i < GameCanvas.menu.menuItems.size(); i++)
		{
			Command command = (Command)GameCanvas.menu.menuItems.elementAt(i);
			if (command != null && NormalizeVietnameseText(command.caption).Contains(text))
			{
				return i;
			}
		}
		return -1;
	}

	public string GetAutoPhaLeLabel()
	{
		return strAutoPhaLe;
	}

	public string GetAutoPhaLeBatchName()
	{
		switch (autoPhaLeBatchMode)
		{
		case 1:
			return "1 lần";
		case 2:
			return "10 lần";
		case 3:
			return "100 lần";
		default:
			return "Tắt";
		}
	}

	public void StopAutoPhaLe()
	{
		isAutoPhaLe = false;
		ResetAutoPhaLeState();
	}

	public void StopAutoPhaLeFromButton()
	{
		isAutoPhaLe = false;
		pendingAutoSaleThoiVangIndex = -1;
		ResetAutoPhaLeState();
		CloseAutoPhaLeInputDialogs();
		GameScr.info1.addInfo("Đã dừng tự động pha lê hóa", 0);
	}

	private void ResetAutoPhaLeState()
	{
		itemPhale = null;
		maxPhale = -1;
		currPhale = -1;
		pendingAutoSaleThoiVangIndex = -1;
		CloseAutoPhaLeInputDialogs();
	}

	public void CloseAutoPhaLeInputDialogs()
	{
		if (GameCanvas.panel != null && GameCanvas.panel.chatTField != null)
		{
			GameCanvas.panel.chatTField.isShow = false;
		}
		if (ChatTextField.gI() != null)
		{
			ChatTextField.gI().isShow = false;
		}
		GameScr.instance.switchToMe();
		ClientInput.gI().clearScreen();
		GameCanvas.endDlg();
	}

	public void AutoSubmitSaleThoiVangClientInput()
	{
		ClientInput clientInput = ClientInput.gI();
		if (clientInput != null && clientInput.tf != null)
		{
			string text = ((slThoiVangCanBan > 0) ? slThoiVangCanBan : 1).ToString();
			for (int i = 0; i < clientInput.tf.Length; i++)
			{
				clientInput.tf[i].setText(text);
			}
			Service.gI().sendClientInput(clientInput.tf);
		}
		CloseAutoPhaLeInputDialogs();
		if (pendingAutoSaleThoiVangIndex >= 0)
		{
			short id = (short)pendingAutoSaleThoiVangIndex;
			pendingAutoSaleThoiVangIndex = -1;
			Service.gI().saleItem(1, 1, id);
		}
	}

	public void ToggleAutoPhaLe()
	{
		if (isAutoPhaLe)
		{
			StopAutoPhaLe();
		}
		else
		{
			isAutoPhaLe = true;
			new Thread(AutoPhaLe).Start();
		}
	}

	public void CycleAutoPhaLeBatchModeConfig()
	{
		autoPhaLeBatchMode = (autoPhaLeBatchMode % 3) + 1;
		Rms.saveRMSInt("autoPhaLeBatchMode", autoPhaLeBatchMode);
	}

	private int GetAutoPhaLeConfirmMenuIndex()
	{
		if (autoPhaLeBatchMode == 2)
		{
			int num = FindMenuIndexByCaption("10 lan");
			if (num >= 0)
			{
				return num;
			}
		}
		if (autoPhaLeBatchMode == 3)
		{
			int num2 = FindMenuIndexByCaption("100 lan");
			if (num2 >= 0)
			{
				return num2;
			}
		}
		if (GameCanvas.menu == null || GameCanvas.menu.menuItems == null)
		{
			return 0;
		}
		for (int i = 0; i < GameCanvas.menu.menuItems.size(); i++)
		{
			Command command = (Command)GameCanvas.menu.menuItems.elementAt(i);
			if (command == null)
			{
				continue;
			}
			string text = NormalizeVietnameseText(command.caption);
			if (!text.Contains("nang cap"))
			{
				continue;
			}
			if (text.Contains("10 lan") || text.Contains("100 lan"))
			{
				continue;
			}
			return i;
		}
		return 0;
	}

	private static bool MenuContainsCaption(string[] menu, string caption)
	{
		if (menu == null)
		{
			return false;
		}
		for (int i = 0; i < menu.Length; i++)
		{
			if (ContainsNormalizedText(menu[i], caption))
			{
				return true;
			}
		}
		return false;
	}

	private static bool IsNhapNgocRongItem(Item item)
	{
		return item != null && item.template != null && item.template.id > 14 && item.template.id <= 20;
	}

	private static bool IsNhapNgocRongPanel(Panel panel)
	{
		return panel != null && panel.type == 12 && (ContainsNormalizedText(panel.combineTopInfo, "7 vien ngoc rong") || (ContainsNormalizedText(panel.combineTopInfo, "ngoc rong") && ContainsNormalizedText(panel.combineInfo, "cung sao")));
	}

	public static bool IsPhaLeHoaPanel(Panel panel)
	{
		return panel != null && panel.type == 12 && ContainsNormalizedText(panel.combineTopInfo, "trang bị pha lê");
	}

	private int GetAutoDapDoInterval()
	{
		float num = Mathf.Max(1f, Time.timeScale);
		return Mathf.Max(AutoDapDoMinInterval, Mathf.RoundToInt((float)AutoDapDoBaseInterval / num));
	}

	public void RequestStartAutoDapDo()
	{
		if (isAutoDapDo)
		{
			StopAutoDapDo("Đã tắt auto đập đồ");
			return;
		}
		StartAutoDapDo();
	}

	public void StartAutoDapDo()
	{
		if (!IsDapDoUpgradePanel(GameCanvas.panel))
		{
			GameScr.info1.addInfo("Hãy mở tab nâng cấp trang bị tại Bà Hạt Mít", 0);
			return;
		}
		Item upgradeItem = GetDapDoUpgradeItem(GameCanvas.panel);
		if (upgradeItem == null)
		{
			GameScr.info1.addInfo("Không tìm thấy trang bị cần đập", 0);
			return;
		}
		if (GameCanvas.panel.vItemCombine == null || GameCanvas.panel.vItemCombine.size() < 2)
		{
			GameScr.info1.addInfo("Hãy đặt trang bị và đá nâng cấp trước khi bật auto", 0);
			return;
		}
		autoDapDoTargetLevel = GetItemUpgradeLevel(upgradeItem) + 1;
		isAutoDapDo = true;
		autoDapDoUpgradeCount = 0;
		isWaitingAutoDapDoMenu = false;
		lastAutoDapDo = 0L;
		lastAutoDapDoCombineRequest = 0L;
		CloseDapDoNpcUi();
		GameScr.info1.addInfo("Đã bật auto đập đồ (dừng khi lên +" + autoDapDoTargetLevel + ")", 0);
	}

	public void StopAutoDapDo(string message = null)
	{
		isAutoDapDo = false;
		autoDapDoTargetLevel = -1;
		isWaitingAutoDapDoMenu = false;
		CloseDapDoNpcUi();
		if (!string.IsNullOrEmpty(message))
		{
			GameScr.info1.addInfo(message, 0);
		}
	}

	public bool ShouldSuppressDapDoConfirmUi()
	{
		return isAutoDapDo;
	}

	private static void CloseDapDoNpcUi()
	{
		InfoDlg.hide();
		Char.isLockKey = false;
		GameScr.info1.isUpdate = true;
		if (GameCanvas.menu != null && GameCanvas.menu.showMenu)
		{
			GameCanvas.menu.doCloseMenu();
		}
		ChatPopup.currChatPopup = null;
		ChatPopup.serverChatPopUp = null;
		Char.chatPopup = null;
		if (GameCanvas.panel != null)
		{
			GameCanvas.panel.cp = null;
		}
	}

	private string GetAutoDapDoPhaseText()
	{
		if (isWaitingAutoDapDoMenu)
		{
			return "Chờ xác nhận...";
		}
		if (GameCanvas.panel != null && (!GameCanvas.panel.isDoneCombine || GameCanvas.panel.combineSuccess != -1))
		{
			return "Đang nâng cấp...";
		}
		return "Sẵn sàng";
	}

	private static int FindDapDoUpgradeMenuIndex()
	{
		if (GameCanvas.menu == null || GameCanvas.menu.menuItems == null)
		{
			return -1;
		}
		for (int i = 0; i < GameCanvas.menu.menuItems.size(); i++)
		{
			Command command = (Command)GameCanvas.menu.menuItems.elementAt(i);
			if (command == null)
			{
				continue;
			}
			string caption = NormalizeVietnameseText(command.caption);
			if (caption.Contains("nang cap") && !caption.Contains("tu choi"))
			{
				return i;
			}
		}
		return -1;
	}

	private bool TryConfirmDapDoMenu()
	{
		if (GameCanvas.menu == null || !GameCanvas.menu.showMenu || !IsDapDoUpgradePanel(GameCanvas.panel))
		{
			return false;
		}
		int menuIndex = FindDapDoUpgradeMenuIndex();
		if (menuIndex < 0)
		{
			return false;
		}
		lastAutoDapDoCombineRequest = mSystem.currentTimeMillis();
		Service.gI().confirmMenu(AutoDapDoNpcId, (sbyte)menuIndex);
		GameCanvas.menu.doCloseMenu();
		CloseDapDoNpcUi();
		autoDapDoUpgradeCount++;
		return true;
	}

	private void AutoDapDo(long currentTime)
	{
		if (!isAutoDapDo)
		{
			return;
		}
		Panel panel = GameCanvas.panel;
		if (panel == null || panel.type != 12 || !panel.isShow)
		{
			StopAutoDapDo("Hãy mở giao diện nâng cấp trang bị tại Bà Hạt Mít");
			return;
		}
		Item upgradeItem = GetDapDoUpgradeItem(panel);
		if (upgradeItem == null)
		{
			StopAutoDapDo("Đã dừng auto đập đồ vì không còn trang bị");
			return;
		}
		if (GetItemUpgradeLevel(upgradeItem) >= autoDapDoTargetLevel)
		{
			StopAutoDapDo("Đã lên +" + GetItemUpgradeLevel(upgradeItem) + ", tự động tắt auto đập đồ");
			return;
		}
		if (panel.vItemCombine == null || panel.vItemCombine.size() < 2)
		{
			StopAutoDapDo("Thiếu trang bị hoặc nguyên liệu");
			return;
		}
		if (TryConfirmDapDoMenu())
		{
			return;
		}
		if (!panel.isDoneCombine || panel.combineSuccess != -1)
		{
			isWaitingAutoDapDoMenu = false;
			return;
		}
		if (isWaitingAutoDapDoMenu)
		{
			if (currentTime - lastAutoDapDoCombineRequest < AutoDapDoRequestTimeout)
			{
				return;
			}
			isWaitingAutoDapDoMenu = false;
		}
		isWaitingAutoDapDoMenu = true;
		lastAutoDapDoCombineRequest = currentTime;
		InfoDlg.showWait();
		Service.gI().combine(1, panel.vItemCombine);
	}

	private Item GetSelectedNhapNgocRongItem(Panel panel)
	{
		if (panel == null || panel.vItemCombine == null || panel.vItemCombine.size() != 1)
		{
			return null;
		}
		Item item = (Item)panel.vItemCombine.elementAt(0);
		if (!IsNhapNgocRongItem(item))
		{
			return null;
		}
		return FindItemBagWithIndexUI(item.indexUI) ?? item;
	}

	private static bool IsBaHatMitMap()
	{
		for (int i = 0; i < AutoNhapNgocRongMapIds.Length; i++)
		{
			if (TileMap.mapID == AutoNhapNgocRongMapIds[i])
			{
				return true;
			}
		}
		return false;
	}

	private bool CanUseAutoNhapNgocRong(out Panel panel, out Item selectedNhapNgocRongItem, out string message, out bool shouldStop)
	{
		panel = GameCanvas.panel;
		selectedNhapNgocRongItem = null;
		message = string.Empty;
		shouldStop = false;
		if (Char.myCharz() == null || Char.myCharz().arrItemBag == null)
		{
			return false;
		}
		if (!IsBaHatMitMap() && GetNpcByTempId(21) == null)
		{
			message = "Bà Hạt Mít chỉ ở 3 Vách Núi và Siêu Thị";
			return false;
		}
		if (panel == null || panel.type != 12 || !IsNhapNgocRongPanel(panel))
		{
			message = "Hãy mở giao diện nhập ngọc rồng tại Bà Hạt Mít";
			return false;
		}
		selectedNhapNgocRongItem = GetSelectedNhapNgocRongItem(panel);
		if (selectedNhapNgocRongItem == null)
		{
			message = "Hãy đặt đúng 1 loại ngọc rồng trước khi bật auto";
			return false;
		}
		if (selectedNhapNgocRongItem.quantity < 7)
		{
			message = "Không đủ 7 viên ngọc rồng để tiếp tục";
			shouldStop = true;
			return false;
		}
		return true;
	}

	private bool TryConfirmNhapNgocRongMenu()
	{
		if (GameCanvas.menu == null || !GameCanvas.menu.showMenu || !IsNhapNgocRongPanel(GameCanvas.panel))
		{
			return false;
		}
		int num = FindMenuIndexByCaption("lam phep");
		if (num < 0 || GetSelectedNhapNgocRongItem(GameCanvas.panel) == null)
		{
			return false;
		}
		lastAutoNhapNgocRongCombineRequest = mSystem.currentTimeMillis();
		Service.gI().confirmMenu(21, (sbyte)num);
		GameCanvas.menu.doCloseMenu();
		ChatPopup.currChatPopup = null;
		ChatPopup.serverChatPopUp = null;
		Char.chatPopup = null;
		if (GameCanvas.panel != null)
		{
			GameCanvas.panel.cp = null;
		}
		return true;
	}

	public bool ShouldAddNhapNgocRongMenuCommand(string[] menu, Npc npc)
	{
		return npc != null && npc.template != null && npc.template.npcTemplateId == 21 && IsNhapNgocRongPanel(GameCanvas.panel) && MenuContainsCaption(menu, "lam phep") && MenuContainsCaption(menu, "tu choi");
	}

	public Command CreateNhapNgocRongMenuCommand(Npc npc)
	{
		return new Command(isAutoNhapNgocRong ? StopAutoNhapNgocRongMenuCaption : AutoNhapNgocRongMenuCaption, this, ActionToggleAutoNhapNgocRongMenu, npc);
	}

	private void HandleNhapNgocRongMenuCommand()
	{
		if (isAutoNhapNgocRong)
		{
			StopAutoNhapNgocRong("Đã dừng auto nhập ngọc rồng");
			return;
		}
		if (!CanUseAutoNhapNgocRong(out var panel, out _, out var message, out _))
		{
			if (!string.IsNullOrEmpty(message))
			{
				GameScr.info1.addInfo(message, 0);
			}
			return;
		}
		SetAutoNhapNgocRong(true);
		lastAutoNhapNgocRong = mSystem.currentTimeMillis();
		GameScr.info1.addInfo("Đã bật auto nhập ngọc rồng", 0);
		if (!TryConfirmNhapNgocRongMenu())
		{
			isWaitingAutoNhapNgocRongMenu = true;
			lastAutoNhapNgocRongCombineRequest = lastAutoNhapNgocRong;
			InfoDlg.showWait();
			Service.gI().combine(1, panel.vItemCombine);
		}
	}

	private void AutoNhapNgocRong(long currentTime)
	{
		if (GameCanvas.menu != null && GameCanvas.menu.showMenu)
		{
			if (TryConfirmNhapNgocRongMenu())
			{
				return;
			}
			if (IsNhapNgocRongPanel(GameCanvas.panel))
			{
				StopAutoNhapNgocRong("Đã dừng auto nhập ngọc rồng. Hãy kiểm tra hành trang và nguyên liệu.");
			}
			return;
		}
		if (GameCanvas.panel != null && (!GameCanvas.panel.isDoneCombine || GameCanvas.panel.combineSuccess != -1))
		{
			isWaitingAutoNhapNgocRongMenu = false;
			return;
		}
		if (!CanUseAutoNhapNgocRong(out var panel, out _, out var message, out var shouldStop))
		{
			isWaitingAutoNhapNgocRongMenu = false;
			if (string.IsNullOrEmpty(message))
			{
				return;
			}
			if (shouldStop)
			{
				StopAutoNhapNgocRong(message);
			}
			else
			{
				NotifyAutoNhapNgocRong(message, currentTime);
			}
			return;
		}
		if (isWaitingAutoNhapNgocRongMenu)
		{
			if (currentTime - lastAutoNhapNgocRongCombineRequest < AutoNhapNgocRongRequestTimeout)
			{
				return;
			}
			isWaitingAutoNhapNgocRongMenu = false;
		}
		isWaitingAutoNhapNgocRongMenu = true;
		lastAutoNhapNgocRongCombineRequest = currentTime;
		InfoDlg.showWait();
		Service.gI().combine(1, panel.vItemCombine);
	}

	private void BanVang()
	{
		if (TileMap.mapID != 5)
		{
			GameScr.info1.addInfo("Cần đến Đảo Kame để Tự động bán vàng", 0);
			Thread.Sleep(1000);
			return;
		}
		while (Char.myCharz().xu <= 60000000000L && isAutoPhaLe)
		{
			int index = -1;
			if (Char.myCharz().arrItemBag != null)
			{
				for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
				{
					if (Char.myCharz().arrItemBag[i] != null && Char.myCharz().arrItemBag[i].template.id == 457)
					{
						index = i;
						break;
					}
				}
			}
			if (index == -1)
			{
				GameScr.info1.addInfo("Không tìm thấy thỏi vàng", 0);
				if (isAutoPhaLe)
				{
					StopAutoPhaLe();
					GameScr.info1.addInfo("Vàng không đủ, đã tắt Tự động Pha lê hóa", 0);
				}
				return;
			}
			pendingAutoSaleThoiVangIndex = index;
			Service.gI().saleItem(0, 1, (short)index);
			GameScr.info1.addInfo("Đang bán thỏi vàng", 0);
			for (int j = 0; j < 30 && pendingAutoSaleThoiVangIndex >= 0 && isAutoPhaLe; j++)
			{
				Thread.Sleep(100);
			}
			CloseAutoPhaLeInputDialogs();
		}
		GameScr.info1.addInfo("Đã bán xong", 0);
		Thread.Sleep(500);
	}

	public static Item FindItemBagWithIndexUI(int index)
	{
		Item[] arrItemBag = Char.myCharz().arrItemBag;
		foreach (Item item in arrItemBag)
		{
			if (item != null && item.indexUI == index)
			{
				return item;
			}
		}
		return null;
	}

	public void CollectAllThuongDe()
	{
		isCollectAll = true;
		Service.gI().openMenu(19);
		Service.gI().confirmMenu(19, 2);
		Service.gI().confirmMenu(19, 1);
		Service.gI().buyItem(2, 0, 0);
		Thread.Sleep(2000);
		isCollectAll = false;
	}

	private void OpenMenuThuongDe()
	{
		isOpenThuongDe = true;
		Service.gI().openMenu(19);
		Service.gI().confirmMenu(19, 2);
		Service.gI().confirmMenu(19, 0);
		isOpenThuongDe = false;
	}

	public void quayThuongDe()
	{
		if (isCollectAll || isOpenThuongDe)
		{
			return;
		}
		if (!isPaintThuongDe && TileMap.mapID == 45)
		{
			OpenMenuThuongDe();
		}
		else if (TileMap.mapID == 45)
		{
			if (Input.GetKey("q") || Char.myCharz().xu <= 200000000)
			{
				GameScr.info1.addInfo("Đã tắt Auto VQMM (2)", 0);
				isAutoVQMM = false;
			}
			else
			{
				Service.gI().openMenu(19);
				Service.gI().SendCrackBall(2, 7);
			}
		}
	}

	public bool Chat(string text)
	{
		switch (text)
		{
			case "htl":
				isInventory = !isInventory;
				GameScr.info1.addInfo("Hành Trang Lưới: " + (isInventory ? "ON" : "OFF"), 0);
				return true;
			case "loadskill":
				perform(57, null);
				return true;
			case "ak":
				perform(42, null);
				return true;
			case "ts":
				perform(44, null);
				return true;
			case "tsnguoi":
				perform(48, null);
				return true;
			case "vqmm":
				isAutoVQMM = false;
				GameScr.info1.addInfo("VQMM da duoc tat khoi client", 0);
				return true;
			case "ukhu":
				isUpdateZones = !isUpdateZones;
				GameScr.info1.addInfo("Tự động cập nhật khu: " + (isUpdateZones ? "Bật" : "Tắt"), 0);
				return true;
			default:
				if (text.StartsWith("k "))
				{
					if (int.TryParse(text.Replace("k ", ""), out var khu) && khu >= 0)
					{
						Service.gI().requestChangeZone(khu, -1);
					}
					return true;
				}
				if (text.StartsWith("s "))
				{
					ChangeGameSpeed(text.Replace("s ", ""));
					return true;
				}
				if (text.StartsWith("atc "))
				{
					textAutoChat = text.Replace("atc ", "");
					return true;
				}
				if (text.StartsWith("atctg "))
				{
					textAutoChatTG = text.Replace("atctg ", "");
					return true;
				}
				if (text.StartsWith("do "))
				{
					bossCanDo = text.Replace("do ", "");
					GameScr.info1.addInfo("Boss cần dò: " + bossCanDo, 0);
					return true;
				}
				if (text == "dbx")
				{
					isdoBoss = !isdoBoss;
					GameScr.info1.addInfo("Tự động dò boss: " + (isdoBoss ? "Bật" : "Tắt"), 0);
					return true;
				}
				if (text == "gtv")
				{
					isVietnamese = !isVietnamese;
					GameScr.info1.addInfo("Gõ Tiếng Việt: " + (isVietnamese ? "Bật" : "Tắt"), 0);
					return true;
				}
				return false;
		}
	}

	private void UpdateTouch()
	{
		if (isAutoDapDo && !isEditButton && Panel.imgX != null)
		{
			string textTitle = "[AUTO ĐẬP ĐỒ] " + GetAutoDapDoPhaseText();
			int btnX = GameCanvas.w / 2 + mFont.tahoma_7b_yellow.getWidth(textTitle) / 2 + 5;
			int btnY = 58 + mFont.tahoma_7b_yellow.getHeight() / 2 - Panel.imgX.getHeight() / 2;
			if (GameCanvas.isPointerHoldIn(btnX - 10, btnY - 10, Panel.imgX.getWidth() + 20, Panel.imgX.getHeight() + 20) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				StopAutoDapDo("Đã dừng auto nâng cấp đồ");
				GameCanvas.clearAllPointerEvent();
				return;
			}
		}
		if (isAutoPhaLe && !isEditButton && Panel.imgX != null)
		{
			string textTitle = (itemPhale != null) ? itemPhale.template.name : "Chưa Có";
			int yStart = 10;
			int btnX = GameCanvas.w / 2 + mFont.tahoma_7b_red.getWidth(textTitle) / 2 + 5;
			int btnY = yStart + mFont.tahoma_7b_red.getHeight() / 2 - Panel.imgX.getHeight() / 2;
			if (GameCanvas.isPointerHoldIn(btnX - 10, btnY - 10, Panel.imgX.getWidth() + 20, Panel.imgX.getHeight() + 20) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				StopAutoPhaLeFromButton();
				GameCanvas.clearAllPointerEvent();
				return;
			}
		}
		if (GameScr.gI().isNotPaintTouchControl())
		{
			return;
		}
		if (GameCanvas.isPointerHoldIn(GameScr.imgPanel.getWidth() + 3, 10, GameScr.imgArrow.getWidth() + 2, GameScr.imgArrow.getHeight() + 2) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
		{
			isMenuVisible = true;
			targetMenuX = 22f;
			targetArrowRotation = 180f;
			mGraphics.isFlipping = false;
			SoundMn.gI().buttonClick();
			GameCanvas.clearAllPointerEvent();
			return;
		}
		if (GameCanvas.isPointerHoldIn(GameScr.imgPanel.getWidth() + 65, 10, GameScr.imgArrow2.getWidth() + 2, GameScr.imgArrow2.getHeight() + 2) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
		{
			isMenuVisible = false;
			targetMenuX = 0f;
			targetArrowRotation = 0f;
			SoundMn.gI().buttonClick();
			GameCanvas.clearAllPointerEvent();
			return;
		}
		if (isMenuVisible)
		{
			if (GameCanvas.isPointerHoldIn(GameScr.imgPanel.getWidth() + 8, 3, GameScr.imgModFunc.getWidth() + 2, GameScr.imgModFunc.getHeight() + 2) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				OpenMenu();
				SoundMn.gI().buttonClick();
				GameCanvas.clearAllPointerEvent();
				return;
			}
			if (GameCanvas.isPointerHoldIn(GameScr.imgPanel.getWidth() + 30, 3, GameScr.imgCommandChat.getWidth() + 3, GameScr.imgCommandChat.getHeight() + 2) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				isShowMenuChat = true;
				SoundMn.gI().buttonClick();
				GameCanvas.clearAllPointerEvent();
				return;
			}
		}
		if (isEditButton)
		{
			int buttonWidth = 60;
			int buttonHeight = 24;
			int padding = 10;
			int y = 40;
			int x = GameCanvas.w / 2 - buttonWidth - padding / 2;
			int resetX = GameCanvas.w / 2 + padding / 2;
			if (GameCanvas.isPointerHoldIn(x, y, buttonWidth, buttonHeight) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				SaveButtonPositions();
				isEditButton = false;
				GameScr.info1.addInfo("Đã lưu vị trí các nút", 0);
				GameCanvas.clearAllPointerEvent();
			}
			else if (GameCanvas.isPointerHoldIn(resetX, y, buttonWidth, buttonHeight) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				buttonPositions.Clear();
				InitButtonPositions();
				SaveButtonPositions();
				GameScr.info1.addInfo("Đã reset vị trí các nút về mặc định", 0);
				GameCanvas.clearAllPointerEvent();
			}
			else if (GameCanvas.isPointerDown)
			{
				if (!isDragging)
				{
					foreach (KeyValuePair<string, Point> kvp in buttonPositions)
					{
						int buttonX = modKeyPosX + kvp.Value.x;
						int buttonY = modKeyPosY + kvp.Value.y;
						if (GameCanvas.isPointerHoldIn(buttonX - 16, buttonY - 16, 32, 32))
						{
							selectedButton = kvp.Key;
							dragStart = new Point(GameCanvas.px - buttonX, GameCanvas.py - buttonY);
							isDragging = true;
							GameCanvas.isPointerJustDown = false;
							break;
						}
					}
					return;
				}
				if (selectedButton != null)
				{
					int newX = GameCanvas.px - modKeyPosX - dragStart.x;
					int newY = GameCanvas.py - modKeyPosY - dragStart.y;
					newX = System.Math.Max(-modKeyPosX + 20, System.Math.Min(GameCanvas.w - modKeyPosX - 25, newX));
					newY = System.Math.Max(-modKeyPosY + 20, System.Math.Min(GameCanvas.h - modKeyPosY - 25, newY));
					buttonPositions[selectedButton] = new Point(newX, newY);
				}
			}
			else if (isDragging)
			{
				SaveButtonPositions();
				isDragging = false;
				selectedButton = null;
				dragStart = null;
				GameCanvas.clearAllPointerEvent();
			}
			return;
		}
		foreach (KeyValuePair<string, Point> kvp2 in buttonPositions)
		{
			int num = modKeyPosX + kvp2.Value.x;
			int buttonY2 = modKeyPosY + kvp2.Value.y;
			if (GameCanvas.isPointerHoldIn(num - 16, buttonY2 - 16, 32, 32) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				switch (kvp2.Key)
				{
					case "Capsule":
						UseItem(194);
						break;
					case "Fusion":
						UsePorata();
						break;
					case "Zone":
						userOpenZones = true;
						Service.gI().openUIZone();
						break;
					case "MapLeft":
						ManualXmap.GI().LoadMapLeft();
						break;
					case "MapCenter":
						ManualXmap.GI().LoadMapCenter();
						break;
					case "MapRight":
						ManualXmap.GI().LoadMapRight();
						break;
				}
				GameCanvas.clearAllPointerEvent();
				break;
			}
		}
	}

	public void Update()
	{
		UpdateTouch();
		long currentTime = mSystem.currentTimeMillis();
		if (isPeanPet && currentTime - lastPeanPet >= 3000)
		{
			Char pet = Char.myPetz();
			if (!pet.isDie && (pet.cStamina <= pet.cMaxStamina * 20 / 100 || pet.cHP < pet.cHPFull * 20.0 / 100.0 || pet.cMP < pet.cMPFull * 20.0 / 100.0))
			{
				GameScr.gI().doUseHP();
				lastPeanPet = currentTime;
			}
		}
		if (isAutoPhaLe && itemPhale != null)
		{
			currPhale = GetCurrPhaLe(FindItemBagWithIndexUI(itemPhale.indexUI));
		}
		else
		{
			currPhale = -1;
		}
		if (isAutoChat && currentTime - lastAutoChat >= 4000)
		{
			AutoChat();
			lastAutoChat = currentTime;
		}
		if (isAutoChatTG && currentTime - lastAutoChatTG >= 30000)
		{
			AutoChatTG();
			lastAutoChatTG = currentTime;
		}
		if (!TileMap.isOfflineMap() && currentTime - lastUpdateZones >= 1000)
		{
			UseItemAuto();
			if (isUpdateZones)
			{
				Service.gI().openUIZone();
			}
			lastUpdateZones = currentTime;
		}
		if (isAutoVQMM)
		{
			isAutoVQMM = false;
		}
		if (isAutoNhapNgocRong && currentTime - lastAutoNhapNgocRong >= GetAutoNhapNgocRongInterval())
		{
			AutoNhapNgocRong(currentTime);
			lastAutoNhapNgocRong = currentTime;
		}
		if (isAutoDapDo && currentTime - lastAutoDapDo >= GetAutoDapDoInterval())
		{
			AutoDapDo(currentTime);
			lastAutoDapDo = currentTime;
		}
		if (autoWakeUp && currentTime - lastAutoWakeUp >= 1000)
		{
			AutoHoiSinh();
			lastAutoWakeUp = currentTime;
		}
		if (focusBoss && currentTime - lastFocusBoss >= 500)
		{
			AutoFocusBoss();
			lastFocusBoss = currentTime;
		}
		if (autoAttack)
		{
			AutoAttack();
		}
		UpdateNotifTichXanh();
		if (isAutoNoitai && Input.GetKey("q"))
		{
			isAutoNoitai = false;
			ChiSoNoiTai = -1;
			curSelectIntrinsic = "";
			GameScr.info1.addInfo("Đã dừng auto mở nội tại", 0);
		}
		if (isAutoFilterItem && currentTime - lastFilterTime >= 500)
		{
			DoFilter();
			lastFilterTime = currentTime;
		}
		if (isdoBoss && mSystem.currentTimeMillis() - currDoBoss >= 1000)
		{
			DoBoss();
			currDoBoss = mSystem.currentTimeMillis();
		}
	}

	public void PaintButton(mGraphics g, int xAnchor, int yAnchor)
	{
		if (!Main.isIPhone || !isShowButton || GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || GameScr.gI().isPaintPopup() || GameCanvas.panel.isShow || Char.myCharz().taskMaint.taskId == 0 || ChatTextField.gI().isShow || GameCanvas.currentScreen == MoneyCharge.instance)
		{
			return;
		}
		modKeyPosX = xAnchor;
		modKeyPosY = yAnchor;
		InitButtonPositions();
		foreach (KeyValuePair<string, Point> kvp in buttonPositions)
		{
			string buttonName = kvp.Key;
			Point pos = kvp.Value;
			int buttonX = xAnchor + pos.x;
			int buttonY = yAnchor + pos.y;
			switch (buttonName)
			{
				case "Capsule":
					g.drawImage(GameScr.imgCapsule, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
					if (GameCanvas.isPointerHoldIn(buttonX - 15, buttonY - 15, 30, 30))
					{
						g.drawImage(GameScr.imgCapsuleF, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
					}
					break;
				case "Fusion":
					g.drawImage(GameScr.imgFusion, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
					if (GameCanvas.isPointerHoldIn(buttonX - 15, buttonY - 15, 30, 30))
					{
						g.drawImage(GameScr.imgFusionF, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
					}
					break;
				case "Zone":
					g.drawImage(GameScr.imgChangeZone, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
					if (GameCanvas.isPointerHoldIn(buttonX - 15, buttonY - 15, 30, 30))
					{
						g.drawImage(GameScr.imgChangeZoneF, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
					}
					break;
				case "MapLeft":
					g.drawImage(GameScr.imgNextLeft, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
					if (GameCanvas.isPointerHoldIn(buttonX - 15, buttonY - 15, 30, 30))
					{
						g.drawImage(GameScr.imgNextLeftF, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
					}
					break;
				case "MapCenter":
					g.drawImage(GameScr.imgNextCenter, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
					if (GameCanvas.isPointerHoldIn(buttonX - 15, buttonY - 15, 30, 30))
					{
						g.drawImage(GameScr.imgNextCenterF, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
					}
					break;
				case "MapRight":
					g.drawImage(GameScr.imgNextRight, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
					if (GameCanvas.isPointerHoldIn(buttonX - 15, buttonY - 15, 30, 30))
					{
						g.drawImage(GameScr.imgNextRightF, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
					}
					break;
			}
			if (isEditButton)
			{
				g.setColor((selectedButton == buttonName) ? 16711680 : 16776960);
				g.drawRect(buttonX - 16, buttonY - 16, 32, 32);
				mFont.tahoma_7_yellow.drawStringBorder(g, buttonName, buttonX, buttonY - 30, mFont.CENTER, mFont.tahoma_7_grey);
				Point poss = buttonPositions[buttonName];
				string coords = $"({poss.x},{poss.y})";
				mFont.tahoma_7_yellow.drawStringBorder(g, coords, buttonX, buttonY + 20, mFont.CENTER, mFont.tahoma_7_grey);
			}
		}
	}

	public void Paint(mGraphics g)
	{
		int imgHPWidth = mGraphics.getImageWidth(GameScr.imgHP);
		int imgMPWidth = mGraphics.getImageWidth(GameScr.imgMP);
		mFont.tahoma_7_red.drawStringBorder(g, NinjaUtil.getMoneys(Char.myCharz().cHP), 84 + imgHPWidth / 2, 4, mFont.CENTER, mFont.tahoma_7_grey);
		mFont.tahoma_7_blue1.drawStringBorder(g, NinjaUtil.getMoneys(Char.myCharz().cMP), 84 + imgMPWidth / 2, 17, mFont.CENTER, mFont.tahoma_7_grey);
		int xText = 90;
		int yText = GameScr.gI().cmdMenu.y - 20;
		if (!showInfoMe && !isEditButton && !isAutoNoitai && !QuaNapTuan.isNapTuan)
		{
			mFont.tahoma_7_yellow.drawStringBorder(g, "Time: " + DateTime.Now.ToString("dd/MM/yyyy | HH:mm:ss"), xText, yText + 20, mFont.LEFT, mFont.tahoma_7_grey);
			int num2 = 0;
			if (isShowID)
			{
				mFont.tahoma_7_red.drawStringBorder(g, TileMap.mapName + " [" + TileMap.mapID + "]  - Khu: " + TileMap.zoneID, xText, yText + num2, mFont.LEFT, mFont.tahoma_7_grey);
			}
			else
			{
				mFont.tahoma_7_red.drawStringBorder(g, TileMap.mapName + "  - Khu: " + TileMap.zoneID, xText, yText + num2, mFont.LEFT, mFont.tahoma_7_grey);
			}
			num2 += 10;
			mFont.tahoma_7_red.drawStringBorder(g, "X: " + Char.myCharz().cx + " - Y: " + Char.myCharz().cy, xText, yText + num2, mFont.LEFT, mFont.tahoma_7_grey);
		}
		if (isAutoDapDo && !isEditButton)
		{
			Item autoItem = GetDapDoUpgradeItem(GameCanvas.panel);
			int curLv = (autoItem != null) ? GetItemUpgradeLevel(autoItem) : -1;
			string textTitle = "[AUTO ĐẬP ĐỒ] " + GetAutoDapDoPhaseText();
			mFont.tahoma_7b_yellow.drawStringBorder(g, textTitle, GameCanvas.w / 2, 58, mFont.CENTER, mFont.tahoma_7_grey);
			mFont.tahoma_7b_green.drawStringBorder(g, "Đã nâng: " + autoDapDoUpgradeCount + " lần | +" + curLv + " → +" + autoDapDoTargetLevel, GameCanvas.w / 2, 70, mFont.CENTER, mFont.tahoma_7_grey);
			if (Panel.imgX != null)
			{
				int btnX = GameCanvas.w / 2 + mFont.tahoma_7b_yellow.getWidth(textTitle) / 2 + 5;
				int btnY = 58 + mFont.tahoma_7b_yellow.getHeight() / 2 - Panel.imgX.getHeight() / 2;
				g.drawImage(Panel.imgX, btnX, btnY, 0);
			}
		}
		if (isAutoVQMM && !isEditButton)
		{
			Item tv = FindItemBagWithIndexUI(FindItemIndex(457));
			mFont.tahoma_7b_red.drawString(g, "Ngọc Xanh : " + NinjaUtil.getMoneys(Char.myCharz().luong) + " Ngọc Hồng : " + NinjaUtil.getMoneys(Char.myCharz().luongKhoa), GameCanvas.w / 2, 102, mFont.CENTER);
			mFont.tahoma_7b_red.drawString(g, "Vàng : " + NinjaUtil.getMoneys(Char.myCharz().xu) + " Thỏi Vàng : " + (tv?.quantity ?? 0), GameCanvas.w / 2, 112, mFont.CENTER);
		}
		if (showInfoMe && !isEditButton)
		{
			PaintInfoMe(g, xText, yText);
		}
		if (notifBoss && !isEditButton && !isFilterItem && !isAutoNoitai && !QuaNapTuan.isNapTuan)
		{
			int numX = 38;
			for (int i = 0; i < activeBossNotif.size(); i++)
			{
				((ShowBoss)activeBossNotif.elementAt(i)).PaintBoss(g, GameCanvas.w - 2, numX, mFont.RIGHT);
				numX += 10;
			}
		}
		if (notifKillBoss && !isEditButton && !showInfoMe && !isFilterItem && !isAutoNoitai && !QuaNapTuan.isNapTuan)
		{
			int numXKilledBoss = 65;
			for (int j = 0; j < killedBossNotif.size(); j++)
			{
				((ShowBoss)killedBossNotif.elementAt(j)).PaintBoss(g, 100, numXKilledBoss, mFont.LEFT);
			}
		}
		if (showCharsInMap && !isEditButton)
		{
			PaintCharInMap(g);
		}
		PaintListInfo(g);
		if (lineToBoss)
		{
			for (int k = 0; k < GameScr.vCharInMap.size(); k++)
			{
				Char @char = (Char)GameScr.vCharInMap.elementAt(k);
				if (@char != null && @char.cTypePk == 5 && !@char.cName.StartsWith("Đ"))
				{
					g.setColor(Color.red);
					g.drawLine(Char.myCharz().cx - GameScr.cmx, Char.myCharz().cy - GameScr.cmy, @char.cx - GameScr.cmx, @char.cy - GameScr.cmy);
				}
			}
		}
		if (TileMap.mapID != 51 && TileMap.mapID != 52 && TileMap.mapID != 113 && TileMap.mapID != 112 && TileMap.mapID != 129 && TileMap.mapID != 165)
		{
			PaintLogoGif(g, GameCanvas.hw, GameCanvas.hh / 4, 3);
		}
		if (isMenuVisible)
		{
			menuX += (targetMenuX - menuX) * ANIMATION_SPEED;
			arrowRotation += (targetArrowRotation - arrowRotation) * ANIMATION_SPEED;
			g.drawImageFlipped(GameScr.imgArrow2, GameScr.imgPanel.getWidth() + 65, 10f);
			g.drawImage(GameScr.imgModFunc, (float)GameScr.imgPanel.getWidth() + menuX - 15f, 3f, 0);
			g.drawImage(GameScr.imgCommandChat, (float)GameScr.imgPanel.getWidth() + menuX + 5f, 3f, 0);
		}
		else
		{
			menuX += (0f - menuX) * ANIMATION_SPEED;
			arrowRotation += (0f - arrowRotation) * ANIMATION_SPEED;
			g.drawRegion(GameScr.imgArrow, 0, 0, GameScr.imgArrow.getWidth(), GameScr.imgArrow.getHeight(), (int)arrowRotation, GameScr.imgPanel.getWidth() + 8, 15, 3);
		}
		PaintPlayerTichXanh(g);
		if (isAutoNoitai)
		{
			PaintNoiTai(g);
		}
		if (isEditButton)
		{
			PaintEditButton(g);
		}
		if (isShowFilterList)
		{
			ShowFilterList(g);
		}
		if (isShowMenuChat && !QuaNapTuan.isNapTuan)
		{
			PaintMenuChat(g);
		}
	}

	private void PaintEditButton(mGraphics g)
	{
		int buttonWidth = 60;
		int buttonHeight = 24;
		int padding = 10;
		int y = 40;
		int saveX = GameCanvas.w / 2 - buttonWidth - padding / 2;
		g.setColor(0, 0.7f);
		g.fillRect(saveX, y, buttonWidth, buttonHeight);
		g.setColor(65280);
		g.drawRect(saveX, y, buttonWidth, buttonHeight);
		mFont.tahoma_7b_white.drawString(g, "Lưu", saveX + buttonWidth / 2, y + 5, mFont.CENTER);
		int resetX = GameCanvas.w / 2 + padding / 2;
		g.setColor(0, 0.7f);
		g.fillRect(resetX, y, buttonWidth, buttonHeight);
		g.setColor(16711680);
		g.drawRect(resetX, y, buttonWidth, buttonHeight);
		mFont.tahoma_7b_white.drawString(g, "Reset", resetX + buttonWidth / 2, y + 5, mFont.CENTER);
	}

	public void PaintAutoPhaLe(mGraphics g)
	{
		if (isAutoPhaLe && !isEditButton && GameCanvas.currentScreen == GameScr.gI())
		{
			string textTitle = (itemPhale != null) ? itemPhale.template.name : "Chưa Có";
			int yStart = 10;
			mFont.tahoma_7b_red.drawStringBorder(g, textTitle, GameCanvas.w / 2, yStart, mFont.CENTER, mFont.tahoma_7_grey);
			mFont.tahoma_7b_red.drawStringBorder(g, (itemPhale != null) ? ("Số Sao : " + currPhale) : "Số Sao : -1", GameCanvas.w / 2, yStart + 11, mFont.CENTER, mFont.tahoma_7_grey);
			mFont.tahoma_7b_red.drawStringBorder(g, "Số Sao Cần Đập : " + maxPhale + " Sao | " + GetAutoPhaLeBatchName(), GameCanvas.w / 2, yStart + 22, mFont.CENTER, mFont.tahoma_7_grey);
			if (Panel.imgX != null)
			{
				int btnX = GameCanvas.w / 2 + mFont.tahoma_7b_red.getWidth(textTitle) / 2 + 5;
				int btnY = yStart + mFont.tahoma_7b_red.getHeight() / 2 - Panel.imgX.getHeight() / 2;
				g.drawImage(Panel.imgX, btnX, btnY, 0);
			}
			Item tv = FindItemBagWithIndexUI(FindItemIndex(457));
			mFont.tahoma_7b_red.drawStringBorder(g, "Ngọc Xanh : " + NinjaUtil.getMoneys(Char.myCharz().luong) + " Ngọc Hồng : " + NinjaUtil.getMoneys(Char.myCharz().luongKhoa), GameCanvas.w / 2, yStart + 33, mFont.CENTER, mFont.tahoma_7_grey);
			mFont.tahoma_7b_red.drawStringBorder(g, "Vàng : " + NinjaUtil.getMoneys(Char.myCharz().xu) + " Thỏi Vàng : " + (tv?.quantity ?? 0), GameCanvas.w / 2, yStart + 44, mFont.CENTER, mFont.tahoma_7_grey);
		}
	}

	private void PaintNoiTai(mGraphics g)
	{
		isLogo = false;
		int padding = 5;
		int boxHeight = 15;
		int boxWidth = GameCanvas.w / 2;
		int num = (GameCanvas.w - boxWidth) / 2;
		if (!string.IsNullOrEmpty(currentPlayerNoiTai))
		{
			int y = padding;
			string intrinsicInfo = $"NT: {currentPlayerNoiTai} [{CurrentParamNoitaiPlayer}%]";
			mFont.tahoma_7_yellow.drawStringBorder(g, intrinsicInfo, GameCanvas.w / 2, y + 27, mFont.CENTER, mFont.tahoma_7_grey);
		}
		if (!string.IsNullOrEmpty(CurrentNoiTai))
		{
			int y2 = padding * 2 + boxHeight;
			string autoInfo = $"Auto: {CurrentNoiTai} -> {ChiSoNoiTai}%";
			mFont.tahoma_7_red.drawStringBorder(g, autoInfo, GameCanvas.w / 2, y2 + 30, mFont.CENTER, mFont.tahoma_7_grey);
		}
		int buttonX = num + boxWidth + 5;
		int buttonY = padding * 2 + boxHeight * 2 + 10;
		g.drawImage(imgCloseButton, buttonX - imgCloseButton.getWidth() / 2 - 12, buttonY - imgCloseButton.getHeight() / 2);
		if (GameCanvas.isPointerHoldIn(buttonX - imgCloseButton.getWidth() / 2 - 12, buttonY - imgCloseButton.getHeight() / 2, imgCloseButton.getWidth(), imgCloseButton.getHeight()))
		{
			g.setColor(16711680, 0.2f);
			g.fillRect(buttonX - imgCloseButton.getWidth() / 2 - 12, buttonY - imgCloseButton.getHeight() / 2, imgCloseButton.getWidth(), imgCloseButton.getHeight());
			if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				isAutoNoitai = false;
				ChiSoNoiTai = -1;
				curSelectIntrinsic = "";
				CurrentNoiTai = "";
				GameScr.info1.addInfo("Đã dừng auto mở nội tại", 0);
				GameCanvas.clearAllPointerEvent();
			}
		}
	}

	private void PaintCharInMap(mGraphics g)
	{
		int numX = GameCanvas.w - 2;
		int numY = (notifBoss ? 92 : 50);
		charsInMap.removeAllElements();
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			if (i > 15 || numY > GameScr.yHP - 20)
			{
				g.fillRect(numX - 150, numY + 1, 150, 10, 2721889, 90);
				mFont.tahoma_7_white.drawStringBorder(g, string.Concat(new object[2]
				{
					i + 1,
					" ..."
				}), numX, numY, mFont.RIGHT, mFont.tahoma_7_grey);
				break;
			}
			Char char6 = (Char)GameScr.vCharInMap.elementAt(i);
			if (char6 != null && char6.cName != null && char6.cName.Length > 0 && !char6.isMiniPet && char6.cName.ToLower() != "trọng tài")
			{
				g.fillRect(numX - 150, numY + 1, 150, 10, 2721889, 90);
				string[] str = new string[9]
				{
					(i + 1 < 10) ? "0" : "",
					(i + 1).ToString(),
					". [",
					CharGender(char6),
					"] ",
					char6.cName,
					" [ ",
					NinjaUtil.getMoneys(char6.cHP).ToString(),
					" ]"
				};
				if (char6 == Char.myCharz().charFocus)
				{
					mFont.tahoma_7_yellow.drawStringBorder(g, string.Concat(str), numX, numY, mFont.RIGHT, mFont.tahoma_7_grey);
				}
				else if (char6.charID < 0 && char6.charID > -1000 && char6.charID != -114)
				{
					mFont.tahoma_7_red.drawStringBorder(g, string.Concat(str), numX, numY, mFont.RIGHT, mFont.tahoma_7_grey);
				}
				else if (Char.myCharz().clan != null && char6.clanID == Char.myCharz().clan.ID)
				{
					mFont.tahoma_7_green.drawStringBorder(g, string.Concat(str), numX, numY, mFont.RIGHT, mFont.tahoma_7_grey);
				}
				else
				{
					mFont.tahoma_7_white.drawStringBorder(g, string.Concat(str), numX, numY, mFont.RIGHT, mFont.tahoma_7_grey);
				}
				charsInMap.addElement(char6);
				numY += 10;
			}
		}
	}

	private void PaintListInfo(mGraphics g)
	{
		int num4 = 70;
		Char charFocus = Char.myCharz().charFocus;
		if (charFocus != null && Char.myCharz().isMeCanAttackOtherPlayer(charFocus))
		{
			int healthBarWidth = 150;
			int healthBarHeight = 12;
			int healthBarX = GameCanvas.w / 2 - healthBarWidth / 2;
			int healthBarY = num4;
			g.setColor(8421504);
			g.fillRect(healthBarX - 3, healthBarY - 3, healthBarWidth + 6, healthBarHeight + 6, 12);
			g.setColor(2829099);
			g.fillRect(healthBarX - 1, healthBarY - 1, healthBarWidth + 2, healthBarHeight + 2, 10);
			float healthPercentage = (float)((double)(float)charFocus.cHP / charFocus.cHPFull);
			int currentHealthBarWidth = (int)((float)healthBarWidth * healthPercentage);
			if (healthPercentage > 0.5f)
			{
				g.setColor(65280);
			}
			else if (healthPercentage > 0.25f)
			{
				g.setColor(16776960);
			}
			else
			{
				g.setColor(16711680);
			}
			g.fillRect(healthBarX, healthBarY, currentHealthBarWidth, healthBarHeight, 8);
			string hpText = NinjaUtil.getMoneys(charFocus.cHP) + "/" + NinjaUtil.getMoneys(charFocus.cHPFull);
			mFont.tahoma_7b_white.drawStringBorder(g, hpText, GameCanvas.w / 2 + 1, healthBarY + healthBarHeight / 2 - 6, mFont.CENTER, mFont.tahoma_7_grey);
			num4 += 17;
			if (charFocus.protectEff)
			{
				mFont.tahoma_7b_red.drawString(g, "Đang khiên năng lượng", GameCanvas.w / 2, num4, mFont.CENTER);
				num4 += 10;
			}
			if (charFocus.isMonkey == 1)
			{
				mFont.tahoma_7b_red.drawString(g, "Đang biến khỉ", GameCanvas.w / 2, num4, mFont.CENTER);
				num4 += 10;
			}
			if (charFocus.sleepEff)
			{
				mFont.tahoma_7b_red.drawString(g, "Bị thôi miên", GameCanvas.w / 2, num4, mFont.CENTER);
				num4 += 10;
			}
			if (charFocus.holdEffID != 0)
			{
				mFont.tahoma_7b_red.drawString(g, "Bị trói", GameCanvas.w / 2, num4, mFont.CENTER);
				num4 += 10;
			}
			if (charFocus.isFreez)
			{
				mFont.tahoma_7b_red.drawString(g, "Bị TDHS: " + charFocus.freezSeconds, GameCanvas.w / 2, num4, mFont.CENTER);
				num4 += 10;
			}
			if (charFocus.blindEff)
			{
				mFont.tahoma_7b_red.drawString(g, "Bị choáng", GameCanvas.w / 2, num4, mFont.CENTER);
			}
		}
	}

	private void PaintInfoMe(mGraphics g, int xText, int yText)
	{
		if (mSystem.currentTimeMillis() - lastUpdateInfoMe > 3000)
		{
			Service.gI().petInfo();
			lastUpdateInfoMe = mSystem.currentTimeMillis();
		}
		int num = 10;
		int numy = 64;
		mFont.tahoma_7b_yellow.drawStringBorder(g, "Sư Phụ :", xText, yText, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_white.drawStringBorder(g, "SM: " + NinjaUtil.getMoneys(Char.myCharz().cPower), xText, yText + num, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_white.drawStringBorder(g, "TN: " + NinjaUtil.getMoneys(Char.myCharz().cTiemNang), xText, yText + 2 * num, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_white.drawStringBorder(g, "SĐ: " + NinjaUtil.getMoneys(Char.myCharz().cDamFull), xText, yText + 3 * num, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_white.drawStringBorder(g, "Giáp: " + NinjaUtil.getMoneys(Char.myCharz().cDefull), xText, yText + 4 * num, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7b_yellow.drawStringBorder(g, "Đệ Tử :", xText, yText + numy, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_white.drawStringBorder(g, "SM: " + NinjaUtil.getMoneys(Char.myPetz().cPower), xText, yText + num + numy, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_white.drawStringBorder(g, "TN: " + NinjaUtil.getMoneys(Char.myPetz().cTiemNang), xText, yText + 2 * num + numy, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_white.drawStringBorder(g, "SĐ: " + NinjaUtil.getMoneys(Char.myPetz().cDamFull), xText, yText + 3 * num + numy, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_white.drawStringBorder(g, "HP : " + NinjaUtil.getMoneys(Char.myPetz().cHP), xText, yText + 4 * num + numy, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_white.drawStringBorder(g, "MP : " + NinjaUtil.getMoneys(Char.myPetz().cMP), xText, yText + 5 * num + numy, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_white.drawStringBorder(g, "Giáp: " + NinjaUtil.getMoneys(Char.myPetz().cDefull), xText, yText + 6 * num + numy, mFont.LEFT, mFont.tahoma_7_grey);
	}

	public void perform(int idAction, object p)
	{
		if (idAction == ActionToggleAutoNhapNgocRongMenu)
		{
			HandleNhapNgocRongMenuCommand();
			return;
		}
		if (idAction == ActionStartAutoDapDo)
		{
			StartAutoDapDo();
			return;
		}
		if (idAction <= 60)
		{
			if (idAction <= 8)
			{
				switch (idAction)
				{
					default:
						_ = 8;
						break;
					case 1:
						{
							string notif;
							if (int.TryParse((string)p, out var mapId))
							{
								XmapController.StartRunToMapId(mapId);
								notif = "Di chuyển đến boss ở MAP " + mapId;
							}
							else
							{
								notif = "Địa điểm không hợp lệ!";
							}
							GameScr.info1.addInfo(notif, 0);
							break;
						}
					case 2:
						GameScr.info1.addInfo("Đã huỷ di chuyển đến Boss", 0);
						break;
				}
				return;
			}
			switch (idAction)
			{
				case 16:
					{
						MyVector menuPet = new MyVector();
						menuPet.addElement(new Command(isPeanPet ? "Buff đậu cho đệ [Bật]" : "Buff đậu cho đệ [Tắt]", 17));
						GameCanvas.menu.startAt(menuPet, 4);
						break;
					}
				case 17:
					isPeanPet = !isPeanPet;
					GameScr.info1.addInfo("Buff đậu cho đệ " + (isPeanPet ? "[Bật]" : "[Tắt]"), 0);
					break;
				case 32:
					{
						MyVector myVector2 = new MyVector();
						myVector2.addElement(new Command(notifBoss ? "Thông báo BOSS [Bật]" : "Thông báo BOSS [Tắt]", 46));
						myVector2.addElement(new Command(notifKillBoss ? "Thông báo tiêu diệt BOSS [Bật]" : "Thông báo tiêu diệt BOSS [Tắt]", 58));
						myVector2.addElement(new Command(lineToBoss ? "Kẻ đường tới BOSS [Bật]" : "Đường kẻ tới BOSS [Tắt]", 47));
						myVector2.addElement(new Command(focusBoss ? "Focus BOSS [Bật]" : "Focus BOSS [Tắt]", 52));
						GameCanvas.menu.startAt(myVector2, 4);
						break;
					}
				case 38:
					PickMob.mapGoback = TileMap.mapID;
					PickMob.zoneGoback = TileMap.zoneID;
					PickMob.xGoback = Char.myCharz().cx;
					PickMob.yGoback = Char.myCharz().cy;
					PickMob.isGoBack = !PickMob.isGoBack;
					if (PickMob.isGoBack)
					{
						GameScr.info1.addInfo("Map Goback: " + TileMap.mapName + " | Khu: " + TileMap.zoneID, 0);
						GameScr.info1.addInfo("Tọa độ X: " + PickMob.xGoback + " | Y: " + PickMob.yGoback, 0);
						if (Char.myCharz().cHP <= 0.0 || Char.myCharz().statusMe == 14)
						{
							Service.gI().returnTownFromDead();
							new Thread(PickMob.GoBack).Start();
						}
					}
					GameScr.info1.addInfo("Goback tọa độ " + (PickMob.isGoBack ? "[Bật]" : "[Tắt]"), 0);
					break;
				case 42:
					autoAttack = !autoAttack;
					GameScr.info1.addInfo("Tự đánh " + (autoAttack ? "[Bật]" : "[Tắt]"), 0);
					break;
				case 43:
					PickMob.neSieuQuai = !PickMob.neSieuQuai;
					GameScr.info1.addInfo("Né siêu quái " + (PickMob.neSieuQuai ? "[Bật]" : "[Tắt]"), 0);
					break;
				case 44:
					PickMob.tsPlayer = false;
					PickMob.tanSat = ((p != null) ? ((bool)p) : (!PickMob.tanSat));
					GameScr.info1.addInfo("Tàn sát " + (PickMob.tanSat ? "[Bật]" : "[Tắt]"), 0);
					break;
				case 45:
					{
						MyVector myVector = new MyVector();
						MyVector mobIds = new MyVector();
						for (int i = 0; i < GameScr.vMob.size(); i++)
						{
							Mob mob = (Mob)GameScr.vMob.elementAt(i);
							if (GameScr.gI().isMeCanAttackMob(mob) && !mobIds.contains(mob.templateId) && !PickMob.TypeMobsTanSat.Contains(mob.templateId))
							{
								mobIds.addElement(mob.templateId);
								myVector.addElement(new Command("Tàn sát " + mob.getTemplate().name, 49, mob));
							}
						}
						myVector.addElement(new Command(PickMob.tanSat ? "Tàn sát [Bật]" : "Tàn sát [Tắt]", 44));
						myVector.addElement(new Command(PickMob.tsPlayer ? "Tàn sát\nngười [Bật]" : "Tàn sát\nngười [Tắt]", 48));
						myVector.addElement(new Command(autoAttack ? "Tự đánh [Bật]" : "Tự đánh [Tắt]", 42));
						myVector.addElement(new Command(PickMob.neSieuQuai ? "Né siêu quái [Bật]" : "Né siêu quái [Tắt]", 43));
						myVector.addElement(new Command(PickMob.vuotDiaHinh ? "Vượt địa hình [Bật]" : "Vượt địa hình [Tắt]", 76));
						myVector.addElement(new Command(PickMob.telePem ? "Dịch chuyển\n[Bật]" : "Dịch chuyển\n[Tắt]", 80));
						myVector.addElement(new Command(PickMob.isGoBack ? "Goback Tọa Độ [Bật]" : "Goback Tọa Độ [Tắt]", 38));
						myVector.addElement(new Command("Xoá danh sách tàn sát", 51));
						GameCanvas.menu.startAt(myVector, 4);
						break;
					}
				case 46:
					notifBoss = !notifBoss;
					GameScr.info1.addInfo("Thông báo BOSS " + (notifBoss ? "[Bật]" : "[Tắt]"), 0);
					break;
				case 47:
					lineToBoss = !lineToBoss;
					GameScr.info1.addInfo("Kẻ đường tới BOSS " + (lineToBoss ? "[Bật]" : "[Tắt]"), 0);
					break;
				case 48:
					PickMob.tanSat = false;
					PickMob.tsPlayer = ((p != null) ? ((bool)p) : (!PickMob.tsPlayer));
					GameScr.info1.addInfo("Tàn sát người " + (PickMob.tsPlayer ? "[Bật]" : "[Tắt]"), 0);
					break;
				case 49:
					{
						Mob mobType = (Mob)p;
						if (!PickMob.TypeMobsTanSat.Contains(mobType.templateId))
						{
							PickMob.TypeMobsTanSat.Add(mobType.templateId);
						}
						GameScr.info1.addInfo("Tàn sát " + mobType.getTemplate().name, 0);
						perform(44, true);
						break;
					}
				case 51:
					PickMob.TypeMobsTanSat.Clear();
					GameScr.info1.addInfo("Đã xoá danh sách quái tàn sát!", 0);
					break;
				case 52:
					focusBoss = !focusBoss;
					GameScr.info1.addInfo("Focus BOSS " + (focusBoss ? "[Bật]" : "[Tắt]"), 0);
					break;
				case 53:
					{
						MyVector menuOthers = new MyVector();
						menuOthers.addElement(new Command("Tốc độ\nGame", 54));
						menuOthers.addElement(new Command("Tự động\nChat " + (isAutoChat ? "[Bật]" : "[Tắt]"), 55));
						menuOthers.addElement(new Command("Tự động\nChat Thế\nGiới " + (isAutoChatTG ? "[Bật]" : "[Tắt]"), 56));
						menuOthers.addElement(new Command("Load ô\nskill", 57));
						menuOthers.addElement(new Command(isShowID ? "Show ID [ON]" : "Show ID [OFF]", 59));
						menuOthers.addElement(new Command(isPlayingMusic ? "Tắt nhạc" : "Bật nhạc", 60));
						GameCanvas.menu.startAt(menuOthers, 4);
						break;
					}
				case 54:
					MyChatTextField(ChatTextField.gI(), "Nhập tốc độ game", "1 đến 10");
					break;
				case 55:
					isAutoChat = !isAutoChat;
					GameScr.info1.addInfo("Tự động chat " + (isAutoChat ? "[Bật]" : "[Tắt]"), 0);
					break;
				case 56:
					isAutoChatTG = !isAutoChatTG;
					GameScr.info1.addInfo("Tự động chat thế giới " + (isAutoChatTG ? "[Bật]" : "[Tắt]"), 0);
					break;
				case 57:
					LoadSkillToScreen();
					GameScr.info1.addInfo("Đã load ô skill", 0);
					break;
				case 58:
					notifKillBoss = !notifKillBoss;
					GameScr.info1.addInfo("Thông báo tiêu diệt BOSS " + (notifKillBoss ? "[Bật]" : "[Tắt]"), 0);
					break;
				case 59:
					changeStatusShowID();
					GameScr.info1.addInfo("Show ID " + (isShowID ? "[ON]" : "[OFF]"), 0);
					break;
				case 60:
					Sound.PlayMusic(UnityEngine.Random.Range(0, 3));
					Debug.Log("Music " + musics.Count);
					GameScr.info1.addInfo("Đã bật trình phát nhạc", 0);
					break;
			}
			return;
		}
		switch (idAction)
		{
			case 76:
				PickMob.vuotDiaHinh = !PickMob.vuotDiaHinh;
				GameScr.info1.addInfo("Vượt địa hình " + (PickMob.vuotDiaHinh ? "[Bật]" : "[Tắt]"), 0);
				break;
			case 80:
				PickMob.telePem = !PickMob.telePem;
				GameScr.info1.addInfo("Dịch chuyển đến quái\n" + (PickMob.telePem ? "[Bật]" : "[Tắt]"), 0);
				break;
			case 89:
				{
					MyVector menuAutoPick = new MyVector();
					menuAutoPick.addElement(new Command("Tự động nhặt " + (PickMob.IsAutoPickItems ? "[Bật]" : "[Tắt]"), 90));
					menuAutoPick.addElement(new Command("Nhặt tất cả " + (PickMob.IsPickItemsAll ? "[Bật]" : "[Tắt]"), 91));
					menuAutoPick.addElement(new Command("Nhặt xa\n" + (PickMob.IsPickItemsDis ? "[Bật]" : "[Tắt]"), 92));
					menuAutoPick.addElement(new Command("Xem DS lọc đồ", 93));
					menuAutoPick.addElement(new Command("Tự động lọc đồ", 94));
					GameCanvas.menu.startAt(menuAutoPick, 4);
					break;
				}
			case 90:
				PickMob.IsAutoPickItems = !PickMob.IsAutoPickItems;
				GameScr.info1.addInfo("Tự động nhặt " + (PickMob.IsAutoPickItems ? "[Bật]" : "[Tắt]"), 0);
				break;
			case 91:
				PickMob.IsPickItemsAll = !PickMob.IsPickItemsAll;
				GameScr.info1.addInfo("Nhặt tất cả " + (PickMob.IsPickItemsAll ? "[Bật]" : "[Tắt]"), 0);
				break;
			case 92:
				PickMob.IsPickItemsDis = !PickMob.IsPickItemsDis;
				GameScr.info1.addInfo("Nhặt xa " + (PickMob.IsPickItemsDis ? "[Bật]" : "[Tắt]"), 0);
				break;
			case 93:
				isShowFilterList = !isShowFilterList;
				GameScr.info1.addInfo("Đã mở danh sách lọc đồ", 0);
				break;
			case 94:
				isAutoFilterItem = !isAutoFilterItem;
				GameScr.info1.addInfo("Tự động lọc đồ " + (isAutoFilterItem ? "[Bật]" : "[Tắt]"), 0);
				break;
			case 100:
				{
					string obj = (string)p;
					int.TryParse(obj.Split("-")[0], out indexAutoPoint);
					bool.TryParse(obj.Split("-")[1], out autoPointForPet);
					GameCanvas.panel.hideNow();
					MyChatTextField(ChatTextField.gI(), "Tăng đến mức", "VD: 220000");
					break;
				}
			case 101:
				isOpenAccMAnager = true;
				break;
			case 102:
				{
					Account account = (Account)p;
					Rms.saveRMSString("acc", account.getUsername());
					Rms.saveRMSString("pass", account.getPassword());
					if (GameCanvas.loginScr != null && GameCanvas.currentScreen == GameCanvas.loginScr)
					{
						GameCanvas.loginScr.setUserPass();
					}
					isOpenAccMAnager = false;
					break;
				}
			case 103:
				{
					int index = accounts.IndexOf((Account)p);
					accounts.RemoveAt(index);
					cmdsChooseAcc.RemoveAt(index);
					cmdsDelAcc.RemoveAt(index);
					SaveAcc();
					break;
				}
			case 104:
				isOpenAccMAnager = false;
				break;
			case 500:
			case 501:
				AddOrRemoveAutoItem((Item)p, idAction == 500);
				break;
			case 883:
				XmapController.ShowXmapMenu();
				break;
			case 502:
			case 503:
				AddOrRemoveFilterItem((Item)p, idAction == 502);
				break;
		}
	}

	public void AutoBuyItem(int num, Item itemBuy, sbyte buyType = 3)
	{
		new Thread(new ThreadStart(delegate
		{
			for (int i = 0; i < num; i++)
			{
				Service.gI().buyItem(buyType, itemBuy.template.id, 0);
				Thread.Sleep(200);
			}
			GameScr.info1.addInfo("Đã mua xong " + num + " " + itemBuy.template.name, 0);
		})).Start();
	}

	private void AddOrRemoveAutoItem(Item item, bool isAdd)
	{
		if (isAdd)
		{
			listItemAuto.Add(new ItemAuto(item.template.iconID, item.template.id));
			GameScr.info1.addInfo("Đã thêm " + item.template.name + " vào Auto Item", 0);
			return;
		}
		foreach (ItemAuto itemAuto in listItemAuto)
		{
			if (itemAuto.iconID == item.template.iconID && itemAuto.id == item.template.id)
			{
				listItemAuto.Remove(itemAuto);
				GameScr.info1.addInfo("Đã xóa " + item.template.name + " khỏi Auto Item", 0);
				break;
			}
		}
	}

	public void DoDoubleClickToObj(IMapObject obj)
	{
		if ((obj.Equals(Char.myCharz().npcFocus) || GameScr.gI().mobCapcha == null) && !GameScr.gI().checkClickToBotton(obj))
		{
			GameScr.gI().checkEffToObj(obj, isnew: false);
			Char.myCharz().cancelAttack();
			Char.myCharz().currentMovePoint = null;
			Char.myCharz().cvx = (Char.myCharz().cvy = 0);
			obj.stopMoving();
			GameScr.gI().auto = 10;
			GameScr.gI().doFire(isFireByShortCut: false, skipWaypoint: true);
			GameScr.gI().clickToX = obj.getX();
			GameScr.gI().clickToY = obj.getY();
			GameScr.gI().clickOnTileTop = false;
			GameScr.gI().clickMoving = true;
			GameScr.gI().clickMovingRed = true;
			GameScr.gI().clickMovingTimeOut = 20;
			GameScr.gI().clickMovingP1 = 30;
		}
	}

	public void MyChatTextField(ChatTextField chatTField, string strChat, string strName)
	{
		if (isAutoPhaLe && (strChat == strThoiVangBanMoiLanPrompt || strChat.Contains(strThoiVangBanMoiLan)))
		{
			return;
		}
		chatTField.strChat = strChat;
		chatTField.tfChat.name = strName;
		chatTField.to = string.Empty;
		chatTField.isShow = true;
		chatTField.tfChat.isFocus = true;
		chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
		chatTField.tfChat.setMaxTextLenght(10);
		if (!Main.isPC)
		{
			chatTField.startChat(GameCanvas.panel, string.Empty);
		}
		else if (GameCanvas.isTouch)
		{
			chatTField.tfChat.doChangeToTextBox();
		}
	}

	public void ChangeGameSpeed(string strSpeed)
	{
		if (int.TryParse(strSpeed, out var speed) && speed > 0 && speed <= 10)
		{
			Time.timeScale = speed;
			GameScr.info1.addInfo("Tốc độ game: " + speed, 0);
		}
		else
		{
			GameScr.info1.addInfo("Chỉ nhập số từ 1 đến 10", 0);
		}
	}

	public void TeleportToPlayer(int charID)
	{
		Service.gI().gotoPlayer(charID);
	}

	public void AddNotifTichXanh(string notif)
	{
		listNotifTichXanh.addElement(notif);
		if (!startChat)
		{
			int halfW = GameCanvas.w / 2;
			startChat = true;
			xNotif = halfW + halfW / 2;
			lastUpdateNotif = mSystem.currentTimeMillis();
		}
	}

	private void PaintPlayerTichXanh(mGraphics g)
	{
		if (listNotifTichXanh.size() != 0)
		{
			string st = (string)listNotifTichXanh.elementAt(0);
			int halfW = GameCanvas.w / 2;
			g.setClip(halfW - halfW / 3, 50, halfW / 3 * 2, 12);
			g.fillRect(halfW - halfW / 3, 50, halfW / 3 * 2, 12, 0, 60);
			mFont.tahoma_7_yellow.drawStringBorder(g, st, xNotif, 50, 0, mFont.tahoma_7_grey);
			PaintTicks(g, xNotif - 12, 51);
		}
	}

	private void UpdateNotifTichXanh()
	{
		if (!startChat || mSystem.currentTimeMillis() - lastUpdateNotif < 10)
		{
			return;
		}
		xNotif--;
		string strChat = (string)listNotifTichXanh.elementAt(0);
		lastUpdateNotif = mSystem.currentTimeMillis();
		if (xNotif < GameCanvas.w / 2 - 100 - mFont.tahoma_7_yellow.getWidth(strChat))
		{
			xNotif = GameCanvas.w / 2 + 100;
			listNotifTichXanh.removeElementAt(0);
			if (listNotifTichXanh.size() == 0)
			{
				startChat = false;
			}
		}
	}

	public void SetAutoIntrinsic(int param)
	{
		if (string.IsNullOrEmpty(curSelectIntrinsic))
		{
			GameScr.info1.addInfo("Vui lòng chọn nội tại trước!", 0);
			return;
		}
		if (!int.TryParse(curSelectIntrinsic.Split("đến ")[1].Split("%")[0], out var maxParam))
		{
			GameScr.info1.addInfo("Có lỗi xảy ra khi đọc chỉ số!", 0);
			return;
		}
		if (param <= 0 || param > maxParam)
		{
			GameScr.info1.addInfo($"Chỉ số phải từ 1 đến {maxParam}!", 0);
			return;
		}
		ChiSoNoiTai = param;
		CurrentNoiTai = curSelectIntrinsic.Split(new string[2] { "+", "dưới " }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
		if (string.IsNullOrEmpty(CurrentNoiTai))
		{
			GameScr.info1.addInfo("Có lỗi xảy ra khi xử lý nội tại!", 0);
			return;
		}
		isAutoNoitai = true;
		new Thread(DoAutoNoitai).Start();
	}

	private void DoAutoNoitai()
	{
		try
		{
			while (ChiSoNoiTai != -1 && isAutoNoitai)
			{
				if (string.IsNullOrEmpty(currentPlayerNoiTai))
				{
					Thread.Sleep(500);
					continue;
				}
				if (currentPlayerNoiTai.Contains(CurrentNoiTai) && CurrentParamNoitaiPlayer >= ChiSoNoiTai)
				{
					GameScr.info1.addInfo($"Đã đạt nội tại {CurrentNoiTai} +{CurrentParamNoitaiPlayer}%", 0);
					isAutoNoitai = false;
					ChiSoNoiTai = -1;
					curSelectIntrinsic = "";
					CurrentNoiTai = "";
					break;
				}
				Thread thread = new Thread(new ThreadStart(delegate
				{
					try
					{
						Service.gI().speacialSkill(0);
						Thread.Sleep(100);
						Service.gI().confirmMenu(5, 2);
						Thread.Sleep(100);
						Service.gI().confirmMenu(5, 0);
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
					}
				}));
				thread.Start();
				thread.Join();
				Thread.Sleep(200);
			}
		}
		catch (Exception exception2)
		{
			Debug.LogException(exception2);
			isAutoNoitai = false;
			ChiSoNoiTai = -1;
			CurrentNoiTai = "";
		}
	}

	public void SetIncreasePoint(string strPoint)
	{
		if (int.TryParse(strPoint, out var point) && indexAutoPoint != -1 && point > 0)
		{
			pointIncrease = point;
			new Thread(DoAutoIncreasePoint).Start();
			GameScr.info1.addInfo("Tự động tăng " + strPointTypes[indexAutoPoint] + " đến " + point, 0);
		}
		else
		{
			GameScr.info1.addInfo("Có lỗi xảy ra (100)", 0);
		}
	}

	private void DoAutoIncreasePoint()
	{
		while (indexAutoPoint != -1 && pointIncrease > 0)
		{
			Char @char = (autoPointForPet ? Char.myPetz() : Char.myCharz());
			if (indexAutoPoint switch
			{
				0 => @char.cHPGoc,
				1 => @char.cMPGoc,
				2 => @char.cDamGoc,
				3 => @char.cDefGoc,
				4 => @char.cCriticalGoc,
				_ => 0.0,
			} >= (double)pointIncrease)
			{
				indexAutoPoint = -1;
				pointIncrease = 0;
				GameScr.info1.addInfo("Đã đạt chỉ số yêu cầu", 0);
				break;
			}
			Service.gI().upPotential(autoPointForPet, indexAutoPoint, 100);
			Thread.Sleep(500);
		}
	}

	public void LoadAcc()
	{
		string text = Rms.loadRMSString("accManager");
		if (text != null && !(text.Trim('|') == string.Empty))
		{
			accounts.Clear();
			cmdsChooseAcc.Clear();
			cmdsDelAcc.Clear();
			string[] accs = text.Trim('|').Split('|');
			for (int i = 0; i < accs.Length; i++)
			{
				string[] acc = accs[i].Split('$');
				Account account = new Account(acc[0], acc[1]);
				accounts.Add(account);
				Command cmd = new Command(account.getUsername(), this, 102, account);
				cmd.setType();
				cmdsChooseAcc.Add(cmd);
				Command cmdDel = new Command("Xoá", this, 103, account);
				cmdDel.setTypeDelete();
				cmdsDelAcc.Add(cmdDel);
			}
		}
	}

	public void AddAccount(string user, string pass)
	{
		Account account = new Account(user, pass);
		int index = accounts.IndexOf(account);
		if (index != -1)
		{
			accounts.RemoveAt(index);
		}
		accounts.Insert(0, account);
		for (int i = 5; i < accounts.Count; i++)
		{
			accounts.RemoveAt(i);
		}
		SaveAcc();
	}

	private void SaveAcc()
	{
		string text = "";
		foreach (Account acc in accounts)
		{
			text += string.Join('$', acc.getUsername(), acc.getPassword());
			text += "|";
		}
		Rms.saveRMSString("accManager", text.Trim('|'));
	}

	private void AutoChat()
	{
		if (string.IsNullOrEmpty(textAutoChat))
		{
			GameScr.info1.addInfo("Chưa cài nội dung tự động chat", 0);
		}
		else
		{
			Service.gI().chat(textAutoChat);
		}
	}

	private void AutoChatTG()
	{
		if (string.IsNullOrEmpty(textAutoChatTG))
		{
			GameScr.info1.addInfo("Chưa cài nội dung tự động chat thế giới", 0);
		}
		else
		{
			Service.gI().chatGlobal(textAutoChatTG);
		}
	}

	public static string EncodeStringToByteArrayString(string inputString, string key)
	{
		string byteArrayString = BitConverter.ToString(EncodeToBytes(inputString, key)).Replace("-", "");
		return string.Join("-", SplitByLength(byteArrayString, 2));
	}

	private static byte[] EncodeToBytes(string inputString, string key)
	{
		byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
		byte[] keyBytes = Encoding.UTF8.GetBytes(key);
		byte[] encodedBytes = new byte[inputBytes.Length];
		for (int i = 0; i < inputBytes.Length; i++)
		{
			encodedBytes[i] = (byte)(inputBytes[i] ^ keyBytes[i % keyBytes.Length]);
		}
		return encodedBytes;
	}

	private static string[] SplitByLength(string str, int length)
	{
		int strLength = str.Length;
		int numSegments = (strLength + length - 1) / length;
		string[] segments = new string[numSegments];
		for (int i = 0; i < numSegments; i++)
		{
			int startIndex = i * length;
			int segmentLength = Math.min(length, strLength - startIndex);
			segments[i] = str.Substring(startIndex, segmentLength);
		}
		return segments;
	}

	public static string DecodeByteArrayString(string byteArrayString, string key)
	{
		try
		{
			string[] hexValues = byteArrayString.Split('-');
			string concatenatedHex = string.Join("", hexValues);
			byte[] encodedBytes = new byte[concatenatedHex.Length / 2];
			for (int i = 0; i < encodedBytes.Length; i++)
			{
				encodedBytes[i] = Convert.ToByte(concatenatedHex.Substring(i * 2, 2), 16);
			}
			return DecodeToString(encodedBytes, key);
		}
		catch (Exception)
		{
			return string.Empty;
		}
	}

	private static string DecodeToString(byte[] encodedBytes, string key)
	{
		byte[] keyBytes = Encoding.UTF8.GetBytes(key);
		byte[] decodedBytes = new byte[encodedBytes.Length];
		for (int i = 0; i < encodedBytes.Length; i++)
		{
			decodedBytes[i] = (byte)(encodedBytes[i] ^ keyBytes[i % keyBytes.Length]);
		}
		return Encoding.UTF8.GetString(decodedBytes);
	}

	public static void Log(string text)
	{
		if (isDebugEnable)
		{
			Debug.Log(text);
		}
	}

	public static void WriteLog(string message)
	{
		if (!isDebugEnable)
		{
			return;
		}
		try
		{
			StreamWriter streamWriter = new StreamWriter(new FileStream("log_" + DateTime.Today.ToString("yyyyMMdd") + ".txt", FileMode.OpenOrCreate));
			streamWriter.WriteLine(DateTime.Today.ToString("HH:mm:ss") + ": " + message);
			streamWriter.Flush();
			streamWriter.Close();
		}
		catch (Exception ex)
		{
			Log(ex.Message);
		}
	}

	private void LoadSkillToScreen()
	{
		for (int i = 0; i < Char.myCharz().vSkill.size(); i++)
		{
			Skill skill = (Skill)Char.myCharz().vSkill.elementAt(i);
			if (GameCanvas.isTouch && !Main.isPC)
			{
				for (int j = 0; j < GameScr.onScreenSkill.Length; j++)
				{
					if (GameScr.onScreenSkill[j] == skill)
					{
						GameScr.onScreenSkill[j] = null;
					}
				}
				GameScr.onScreenSkill[i] = skill;
				GameScr.gI().saveonScreenSkillToRMS();
				continue;
			}
			for (int k = 0; k < GameScr.keySkill.Length; k++)
			{
				if (GameScr.keySkill[k] == skill)
				{
					GameScr.keySkill[k] = null;
				}
			}
			GameScr.keySkill[i] = skill;
			GameScr.gI().saveKeySkillToRMS();
		}
	}

	public static string DecodeByteArrayString(string byteArrayString)
	{
		try
		{
			string[] hexValues = byteArrayString.Split('-');
			string concatenatedHex = string.Join("", hexValues);
			byte[] encodedBytes = new byte[concatenatedHex.Length / 2];
			for (int i = 0; i < encodedBytes.Length; i++)
			{
				encodedBytes[i] = Convert.ToByte(concatenatedHex.Substring(i * 2, 2), 16);
			}
			return DecodeToString(encodedBytes, 69.ToString());
		}
		catch (Exception)
		{
			return string.Empty;
		}
	}

	public static void DoChatGlobal()
	{
		GameCanvas.endDlg();
		if (Char.myCharz().checkLuong() < 5)
		{
			GameCanvas.startOKDlg(mResources.not_enough_luong_world_channel);
			return;
		}
		if (GameCanvas.panel.chatTField == null)
		{
			GameCanvas.panel.chatTField = new ChatTextField();
			GameCanvas.panel.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			GameCanvas.panel.chatTField.initChatTextField();
			GameCanvas.panel.chatTField.parentScreen = GameCanvas.panel;
		}
		GameCanvas.panel.chatTField.strChat = mResources.world_channel_5_luong;
		GameCanvas.panel.chatTField.tfChat.name = mResources.CHAT;
		GameCanvas.panel.chatTField.to = string.Empty;
		GameCanvas.panel.chatTField.isShow = true;
		GameCanvas.panel.chatTField.tfChat.isFocus = true;
		GameCanvas.panel.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		if (Main.isWindowsPhone)
		{
			GameCanvas.panel.chatTField.tfChat.strInfo = GameCanvas.panel.chatTField.strChat;
		}
		if (!Main.isPC)
		{
			GameCanvas.panel.chatTField.startChat(GameCanvas.panel, string.Empty);
		}
		else if (GameCanvas.isTouch)
		{
			GameCanvas.panel.chatTField.tfChat.doChangeToTextBox();
		}
	}

	public void GoToBoss(int mapId)
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command("Đi tới\nMAP " + mapId, this, 1, mapId.ToString()));
		myVector.addElement(new Command("Huỷ", this, 2, null));
		GameCanvas.menu.startAt(myVector, 4);
	}

	public void LoadFpsMode()
	{
		fpsMode = Rms.loadRMSInt("fpsMode");
		if (fpsMode < 0 || fpsMode > 2)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				fpsMode = 2;
			}
			else
			{
				fpsMode = ((Rms.loadRMSInt("isHighFps") != 0) ? 1 : 0);
			}
		}
		SyncFpsFlags();
		ApplyTargetFrameRate();
	}

	private void SyncFpsFlags()
	{
		isHighFps = fpsMode > 0;
		isFps120 = fpsMode == 2;
	}

	public string GetFpsModeLabel()
	{
		switch (fpsMode)
		{
		case 2:
			return strHighFps + " 120";
		case 1:
			return strHighFps + " 60";
		default:
			return strHighFps;
		}
	}

	public void CycleFpsMode()
	{
		fpsMode = (fpsMode + 1) % 3;
		SyncFpsFlags();
		Rms.saveRMSInt("fpsMode", fpsMode);
		Rms.saveRMSInt("isHighFps", isHighFps ? 1 : 0);
		ApplyTargetFrameRate();
	}

	public void ChangeFPSTarget()
	{
		Rms.saveRMSInt("fpsMode", fpsMode);
		Rms.saveRMSInt("isHighFps", isHighFps ? 1 : 0);
		ApplyTargetFrameRate();
	}

	private int GetPreferredTargetFrameRate()
	{
		switch (fpsMode)
		{
		case 2:
			return UltraFpsTarget;
		case 1:
			return HighFpsTarget;
		default:
			return LowFpsTarget;
		}
	}

	private static int GetDisplayRefreshRate()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		try
		{
#pragma warning disable 618
			int hz = Screen.currentResolution.refreshRate;
#pragma warning restore 618
			if (hz > 0)
			{
				return hz;
			}
		}
		catch (Exception)
		{
		}
#endif
		return 0;
	}

	private int ResolveTargetFrameRate(int preferred)
	{
		int displayHz = GetDisplayRefreshRate();
		if (displayHz <= 0)
		{
			return preferred;
		}
		if (preferred >= UltraFpsTarget)
		{
			if (displayHz >= UltraFpsTarget)
			{
				return UltraFpsTarget;
			}
			if (displayHz >= 90)
			{
				return 90;
			}
		}
		return Mathf.Min(preferred, displayHz);
	}

	private void ApplyTargetFrameRate()
	{
		QualitySettings.vSyncCount = 0;
		int targetFps = ResolveTargetFrameRate(GetPreferredTargetFrameRate());
		Application.targetFrameRate = targetFps;
		float timeScale = Mathf.Max(Time.timeScale, 0.01f);
		Time.fixedDeltaTime = timeScale / targetFps;
		Time.maximumDeltaTime = Mathf.Max(0.05f, Time.fixedDeltaTime * 3f);
	}

	public void OnGameResume()
	{
		Time.timeScale = 1.5f;
		ApplyTargetFrameRate();
		RefreshScreenSize();
		ApplyBlackCameraBackground();
	}

	public static void RefreshScreenSize()
	{
		ScaleGUI.WIDTH = Screen.width;
		ScaleGUI.HEIGHT = Screen.height;
		if (MotherCanvas.instance == null)
		{
			return;
		}
		MotherCanvas.instance.checkZoomLevel((int)ScaleGUI.WIDTH, (int)ScaleGUI.HEIGHT);
		if (GameCanvas.instance == null)
		{
			return;
		}
		GameCanvas.w = MotherCanvas.instance.getWidthz();
		GameCanvas.h = MotherCanvas.instance.getHeightz();
		GameCanvas.hw = GameCanvas.w / 2;
		GameCanvas.hh = GameCanvas.h / 2;
		GameCanvas.wd3 = GameCanvas.w / 3;
		GameCanvas.hd3 = GameCanvas.h / 3;
		GameCanvas.w2d3 = 2 * GameCanvas.w / 3;
		GameCanvas.h2d3 = 2 * GameCanvas.h / 3;
		GameCanvas.w3d4 = 3 * GameCanvas.w / 4;
		GameCanvas.h3d4 = 3 * GameCanvas.h / 4;
		GameCanvas.wd6 = GameCanvas.w / 6;
		GameCanvas.hd6 = GameCanvas.h / 6;
	}

	public static void ApplyBlackCameraBackground()
	{
		Camera mainCamera = Camera.main;
		if (mainCamera != null)
		{
			mainCamera.backgroundColor = Color.black;
		}
	}

	public static void changeStatusEffectInven()
	{
		if (isEffectInven)
		{
			isEffectInven = false;
			Rms.saveRMSInt("effectinven", isEffectInven ? 1 : 0);
		}
		else
		{
			isEffectInven = true;
			Rms.saveRMSInt("effectinven", isEffectInven ? 1 : 0);
		}
	}

	public static void chanegStatusInventory()
	{
		if (isInventory)
		{
			isInventory = false;
			Rms.saveRMSInt("inventory", isInventory ? 1 : 0);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		else
		{
			isInventory = true;
			Rms.saveRMSInt("inventory", isInventory ? 1 : 0);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
	}

	public static void changeStatusLogo()
	{
		if (isLogo)
		{
			isLogo = false;
			imgLogoBig = null;
			logo = null;
			Rms.saveRMSInt("logo", 0);
			if (isLogoGif)
			{
				isLogoGif = false;
				Rms.saveRMSInt("logogif", 0);
			}
		}
		else
		{
			Rms.saveRMSInt("logo", 1);
			isLogo = true;
			if (isLogoGif)
			{
				LoadLogoGif();
				Rms.saveRMSInt("logogif", 1);
			}
			else
			{
				LoadLogoImages();
			}
		}
	}

	public static void changeStatusBackground()
	{
		if (GiamDungLuong)
		{
			GiamDungLuong = false;
			Rms.saveRMSInt("background", GiamDungLuong ? 1 : 0);
		}
		else
		{
			GiamDungLuong = true;
			Rms.saveRMSInt("background", GiamDungLuong ? 1 : 0);
		}
	}

	public static void changeStatusDeVeNhaKhiTachHT()
	{
		deVeNhaKhiTachHT = !deVeNhaKhiTachHT;
		Rms.saveRMSInt("deVeNhaKhiTachHT", deVeNhaKhiTachHT ? 1 : 0);
	}

	public static void changeStatusAnPlayer()
	{
		if (AnPlayer)
		{
			AnPlayer = false;
			Rms.saveRMSInt("anplayer", AnPlayer ? 1 : 0);
		}
		else
		{
			AnPlayer = true;
			Rms.saveRMSInt("anplayer", AnPlayer ? 1 : 0);
		}
	}

	public static void changeStatusShowID()
	{
		if (isShowID)
		{
			isShowID = false;
			Rms.saveRMSInt("showid", isShowID ? 1 : 0);
		}
		else
		{
			isShowID = true;
			Rms.saveRMSInt("showid", isShowID ? 1 : 0);
		}
	}

	public static void changeStatusLogoGif()
	{
		if (isLogoGif)
		{
			isLogoGif = false;
			Rms.saveRMSInt("logogif", 0);
		}
		else
		{
			isLogoGif = true;
			Rms.saveRMSInt("logogif", 1);
		}
	}

	public static Npc GetNpcByTempId(int tempId)
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			if (npc.template.npcTemplateId == tempId)
			{
				return npc;
			}
		}
		return null;
	}

	public static void LoadLogoImages()
	{
		// imgLogoBig = GameCanvas.loadImage("/logoNormal/nrodavutru.png");
		if (imgLogoBig == null)
		{
			// 	GameScr.info1.addInfo("Không thể load logo!", 0);
			isLogo = false;
			Rms.saveRMSInt("logo", 0);
		}
	}

	public static void LoadLogoGif()
	{
		for (int i = 0; i < FrameGif; i++)
		{
			logos[i] = GameCanvas.loadImage("/GifMenu/love-" + i + ".png");
		}
	}

	public static void PaintLogoGif(mGraphics g, int x, int y, int anchor)
	{
		if (!isLogo)
		{
			return;
		}
		if (isLogoGif)
		{
			int id = GameCanvas.gameTick / 2 % FrameGif;
			if (logos[id] != null)
			{
				g.drawImage(logos[id], x, y, anchor);
			}
		}
		else if (imgLogoBig != null)
		{
			g.drawImage(imgLogoBig, x, y, anchor);
		}
	}

	public static void LoadLogoGifMenu()
	{
		for (int i = 0; i < FrameGifMenu; i++)
		{
			logosMenu[i] = GameCanvas.loadImage("/GifMenu/love-" + i + ".png");
		}
	}

	public static void PaintLogoGifMenu(mGraphics g, int x, int y, int anchor)
	{
		int id = GameCanvas.gameTick / 2 % FrameGifMenu;
		if (logosMenu[id] != null)
		{
			g.drawImage(logosMenu[id], x, y, anchor);
		}
	}

	public static void LoadTickImages()
	{
		for (int i = 0; i < 20; i++)
		{
			ticks[i] = GameCanvas.loadImage("/tick/tick_" + i);
		}
	}

	public static void PaintTicks(mGraphics g, int x, int y)
	{
		int id = GameCanvas.gameTick / 4 % 20;
		if (ticks[id] != null)
		{
			g.drawImage(ticks[id], x, y);
		}
	}

	private static IEnumerator LoadFile(string fullPath)
	{
		string fileUri = "file://" + fullPath;
		using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(fileUri, AudioType.OGGVORBIS);
		www.certificateHandler = new BypassCertificateHandler();
		yield return www.SendWebRequest();
		if (www.result != UnityWebRequest.Result.Success)
		{
			Debug.LogError(www.error);
			yield break;
		}
		AudioClip temp = DownloadHandlerAudioClip.GetContent(www);
		musics.Add(temp);
	}

	public static void InitMusic()
	{
		int fromRms = Rms.loadRMSInt("musicSize");
		musicCount = ((fromRms != -1) ? fromRms : 0);
		for (int i = 0; i < musicCount; i++)
		{
			string fullPath = Rms.GetiPhoneDocumentsPath() + "/music_" + i + ".ogg";
			if (File.Exists(fullPath))
			{
				CoroutineRunner.Instance.RunCoroutine(LoadFile(fullPath));
			}
			else
			{
				Debug.LogWarning("File does not exist: " + fullPath);
			}
		}
	}

	public static bool AutoLogin()
	{
		if (autoLogin == null || autoLogin.waitToNextLogin)
		{
			return false;
		}
		if (!Util.CanDoWithTime(autoLogin.lastTimeWait, 500L))
		{
			return false;
		}
		if (ServerListScreen.ipSelect < 0 || ServerListScreen.ipSelect >= ServerListScreen.address.Length || string.IsNullOrEmpty(ServerListScreen.address[ServerListScreen.ipSelect]) || ServerListScreen.testConnect != 2)
		{
			ServerListScreen.LoadIP();
			if (GameCanvas.serverScreen == null)
			{
				GameCanvas.serverScreen = new ServerListScreen();
			}
			GameCanvas.serverScreen.switchToMe();
			autoLogin.lastTimeWait = mSystem.currentTimeMillis();
			return false;
		}
		if (GameCanvas.currentScreen != GameCanvas.loginScr)
		{
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			autoLogin.lastTimeWait = mSystem.currentTimeMillis();
			return false;
		}
		if (!autoLogin.hasSetUserPass)
		{
			Account account = autoLogin.GetAccWithUsername(accounts);
			if (account.getUsername().Length > 0)
			{
				Rms.saveRMSString("acc", account.getUsername());
				Rms.saveRMSString("pass", account.getPassword());
				GameCanvas.loginScr.setUserPass();
				autoLogin.hasSetUserPass = true;
			}
			autoLogin.lastTimeWait = mSystem.currentTimeMillis();
		}
		GameCanvas.loginScr.doLogin();
		autoLogin.waitToNextLogin = true;
		return true;
	}

	public static string Decrypt(string encryptedText, int keys)
	{
		Debug.Log($"Chuỗi nhận được để giải mã: {encryptedText}");
		if (string.IsNullOrEmpty(encryptedText))
		{
			return string.Empty;
		}
		int padding;
		for (padding = 0; (encryptedText.Length + padding) % 5 != 0; padding++)
		{
		}
		if (padding > 0)
		{
			encryptedText = encryptedText.PadRight(encryptedText.Length + padding, 'u');
		}
		List<byte> result = new List<byte>();
		for (int i = 0; i < encryptedText.Length; i += 5)
		{
			ulong value = 0uL;
			for (int j = 0; j < 5; j++)
			{
				int charIndex = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!#$%&()*+-;<=>?@^_`{|}~".IndexOf(encryptedText[i + j]);
				if (charIndex == -1)
				{

					Debug.LogError($"Ký tự không hợp lệ trong chuỗi mã hóa: {encryptedText[i + j]} tại vị trí {i + j}");
					throw new Exception($"Ký tự không hợp lệ trong chuỗi mã hóa: {encryptedText[i + j]}");
				}
				value = value * 85 + (uint)charIndex;
			}
			result.Add((byte)(value >> 24));
			result.Add((byte)(value >> 16));
			result.Add((byte)(value >> 8));
			result.Add((byte)value);
		}
		if (padding > 0)
		{
			result.RemoveRange(result.Count - padding, padding);
		}
		byte[] array = result.ToArray();
		byte[] salt = new byte[16];
		byte[] iv = new byte[16];
		byte[] cipherText = new byte[array.Length - 32];
		Buffer.BlockCopy(array, 0, salt, 0, 16);
		Buffer.BlockCopy(array, 16, iv, 0, 16);
		Buffer.BlockCopy(array, 32, cipherText, 0, cipherText.Length);
		string keysStr = keys.ToString();
		byte[] keyBytes = Encoding.UTF8.GetBytes(keysStr).Concat(salt).ToArray();
		byte[] key = new byte[32];
		for (int k = 0; k < 32; k++)
		{
			key[k] = keyBytes[k % keyBytes.Length];
		}
		using Aes aes = Aes.Create();
		aes.Key = key;
		aes.IV = iv;
		aes.Mode = CipherMode.CBC;
		aes.Padding = PaddingMode.PKCS7;
		using ICryptoTransform decryptor = aes.CreateDecryptor();
		using MemoryStream msDecrypt = new MemoryStream(cipherText);
		using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
		using StreamReader srDecrypt = new StreamReader(csDecrypt);
		return srDecrypt.ReadToEnd();
	}

	public void UpdateIntrinsicInfo(string info)
	{
		if (mSystem.currentTimeMillis() - lastTimeUpdateNoiTai < 500)
		{
			return;
		}
		lastTimeUpdateNoiTai = mSystem.currentTimeMillis();
		try
		{
			if (info.Contains("+"))
			{
				string[] parts = info.Split('+');
				currentPlayerNoiTai = parts[0].Trim();
				if (int.TryParse(parts[1].Split('%')[0], out var value))
				{
					CurrentParamNoitaiPlayer = value;
				}
			}
			else if (info.Contains("dưới"))
			{
				string[] parts2 = info.Split(new string[1] { "dưới " }, StringSplitOptions.None);
				currentPlayerNoiTai = parts2[0].Trim();
				if (int.TryParse(parts2[1].Split('%')[0], out var value2))
				{
					CurrentParamNoitaiPlayer = value2;
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	private void SaveButtonPositions()
	{
		string posData = "";
		foreach (KeyValuePair<string, Point> kvp in buttonPositions)
		{
			posData += $"{kvp.Key},{kvp.Value.x},{kvp.Value.y};";
		}
		Rms.saveRMSString("buttonPositions", posData);
	}

	private void LoadButtonPositions()
	{
		string posData = Rms.loadRMSString("buttonPositions");
		if (!string.IsNullOrEmpty(posData))
		{
			buttonPositions.Clear();
			string[] array = posData.Split(';');
			foreach (string button in array)
			{
				if (string.IsNullOrEmpty(button))
				{
					continue;
				}
				string[] parts = button.Split(',');
				if (parts.Length == 3)
				{
					string name = parts[0];
					if (int.TryParse(parts[1], out var x) && int.TryParse(parts[2], out var y))
					{
						buttonPositions[name] = new Point(x, y);
					}
				}
			}
		}
		else
		{
			InitButtonPositions();
		}
	}

	public static void changeStatusEditButton()
	{
		if (isEditButton)
		{
			isEditButton = false;
			Rms.saveRMSInt("editbutton", 0);
			GameScr.info1.addInfo("Đã tắt chế độ chỉnh sửa nút", 0);
		}
		else
		{
			isEditButton = true;
			GameCanvas.panel.isShow = false;
			Rms.saveRMSInt("editbutton", 1);
			GameScr.info1.addInfo("Đã bật chế độ chỉnh sửa nút", 0);
		}
	}

	private void AddOrRemoveFilterItem(Item item, bool isAdd)
	{
		if (isAdd)
		{
			listFilterItems.Add(new ItemAutoFilter(item.template.iconID, item.template.id, item.template.name));
			GameScr.info1.addInfo("Đã thêm " + item.template.name + " vào DS lọc đồ", 0);
			return;
		}
		foreach (ItemAutoFilter itemFilter in listFilterItems)
		{
			if (itemFilter.iconID == item.template.iconID && itemFilter.id == item.template.id && itemFilter.name == item.template.name)
			{
				listFilterItems.Remove(itemFilter);
				GameScr.info1.addInfo("Đã xóa " + item.template.name + " khỏi DS lọc đồ", 0);
				break;
			}
		}
	}

	private void ShowFilterList(mGraphics g)
	{
		int itemHeight = 25;
		int padding = 10;
		int headerHeight = 30;
		int resizeHandleSize = 40;
		int resizeX = panelX + panelW - resizeHandleSize;
		int resizeY = panelY + panelH - resizeHandleSize;
		if (GameCanvas.isPointerDown && GameCanvas.isPointerHoldIn(panelX, panelY, panelW, headerHeight))
		{
			GameCanvas.isPointerJustDown = false;
			if (!isDragging)
			{
				isDragging = true;
				lastMouseX = GameCanvas.px;
				lastMouseY = GameCanvas.py;
				lastPanelX = panelX;
				lastPanelY = panelY;
			}
			else
			{
				float smoothFactor = 1f;
				int deltaX = GameCanvas.px - lastMouseX;
				int deltaY = GameCanvas.py - lastMouseY;
				int targetX = lastPanelX + deltaX;
				int targetY = lastPanelY + deltaY;
				panelX = (int)((float)panelX + (float)(targetX - panelX) * smoothFactor);
				panelY = (int)((float)panelY + (float)(targetY - panelY) * smoothFactor);
				lastMouseX = GameCanvas.px;
				lastMouseY = GameCanvas.py;
				lastPanelX = panelX;
				lastPanelY = panelY;
			}
			panelX = System.Math.Max(0, System.Math.Min(GameCanvas.w - panelW, panelX));
			panelY = System.Math.Max(0, System.Math.Min(GameCanvas.h - panelH, panelY));
		}
		else
		{
			isDragging = false;
		}
		if (GameCanvas.isPointerDown && !isDragging)
		{
			bool num = GameCanvas.px >= panelX + panelW - resizeHandleSize && GameCanvas.px <= panelX + panelW;
			bool isInResizeZoneY = GameCanvas.py >= panelY + panelH - resizeHandleSize && GameCanvas.py <= panelY + panelH;
			if ((num && GameCanvas.py >= panelY + panelH - resizeHandleSize) || (isInResizeZoneY && GameCanvas.px >= panelX + panelW - resizeHandleSize))
			{
				GameCanvas.isPointerJustDown = false;
				if (!isResizing)
				{
					isResizing = true;
					lastMouseX = GameCanvas.px;
					lastMouseY = GameCanvas.py;
					lastPanelW = panelW;
					lastPanelH = panelH;
				}
				else
				{
					float smoothFactor2 = 1f;
					int deltaX2 = GameCanvas.px - lastMouseX;
					int deltaY2 = GameCanvas.py - lastMouseY;
					int targetW = lastPanelW + deltaX2;
					int targetH = lastPanelH + deltaY2;
					panelW = (int)((float)panelW + (float)(targetW - panelW) * smoothFactor2);
					panelH = (int)((float)panelH + (float)(targetH - panelH) * smoothFactor2);
					lastMouseX = GameCanvas.px;
					lastMouseY = GameCanvas.py;
					lastPanelW = panelW;
					lastPanelH = panelH;
					panelW = System.Math.Max(180, System.Math.Min(GameCanvas.w - panelX, panelW));
					panelH = System.Math.Max(120, System.Math.Min(GameCanvas.h - panelY, panelH));
				}
			}
		}
		else if (!GameCanvas.isPointerDown)
		{
			isResizing = false;
		}
		g.setColor(0, 0.7f);
		g.fillRect(panelX, panelY, panelW, panelH, 5);
		for (int i = 0; i < 3; i++)
		{
			g.setColor(16777215, 0.2f - (float)i * 0.05f);
			g.drawRect(panelX + i, panelY + i, panelW - i * 2, panelH - i * 2);
		}
		g.setColor(16777215, 0.8f);
		for (int j = 0; j < 3; j++)
		{
			g.drawLine(resizeX + 5, panelY + panelH - 10 - j * 5, panelX + panelW - 5, panelY + panelH - 10 - j * 5);
		}
		for (int k = 0; k < 3; k++)
		{
			g.drawLine(panelX + panelW - 10 - k * 5, resizeY + 5, panelX + panelW - 10 - k * 5, panelY + panelH - 5);
		}
		int btnSize = 16;
		g.setColor(16733525);
		g.fillRect(panelX + panelW - btnSize - 5, panelY + 5, btnSize, btnSize, 5);
		mFont.tahoma_7b_white.drawString(g, "X", panelX + panelW - btnSize / 2 - 5, panelY + 7, mFont.CENTER);
		int autoFilterBtnW = 80;
		int autoFilterBtnH = 20;
		int autoFilterBtnX = panelX + panelW - 80;
		int autoFilterBtnY = panelY + panelH - autoFilterBtnH + 25;
		g.setColor(isAutoFilterItem ? 65280 : 16711680);
		g.fillRect(autoFilterBtnX, autoFilterBtnY, autoFilterBtnW, autoFilterBtnH, 8);
		string btnText = (isAutoFilterItem ? "Auto: Bật" : "Auto: Tắt");
		mFont.tahoma_7b_white.drawStringBorder(g, btnText, autoFilterBtnX + autoFilterBtnW / 2 + 1, autoFilterBtnY + 6 + 1, mFont.CENTER, mFont.tahoma_7_grey);
		if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease && GameCanvas.isPointerHoldIn(autoFilterBtnX, autoFilterBtnY, autoFilterBtnW, autoFilterBtnH))
		{
			isAutoFilterItem = !isAutoFilterItem;
			GameScr.info1.addInfo(isAutoFilterItem ? "Đã bật auto lọc đồ" : "Đã tắt auto lọc đồ", 0);
			GameCanvas.clearAllPointerEvent();
		}
		string title = "Danh sách vật phẩm lọc";
		int titleY = panelY + padding;
		mFont.tahoma_7b_white.drawString(g, title, panelX + panelW / 2, titleY, mFont.CENTER);
		g.setColor(5987163);
		g.fillRect(panelX + padding, titleY + 12, panelW - padding * 2, 1, 5);
		g.setClip(panelX, panelY + 35, panelW, panelH - 45);
		int contentHeight = listFilterItems.Count * itemHeight;
		int visibleHeight = panelH - 45;
		int maxScroll = System.Math.Max(0, contentHeight - visibleHeight);
		if (GameCanvas.isPointerDown && !isDragging && !isResizing)
		{
			GameCanvas.isPointerJustDown = false;
			if (!isScrolling)
			{
				isScrolling = true;
				lastMouseY = GameCanvas.py;
				lastScrollY = scrollY;
			}
			else
			{
				float smoothFactor3 = 1f;
				int deltaY3 = lastMouseY - GameCanvas.py;
				int targetScroll = lastScrollY + deltaY3;
				scrollY = (int)((float)scrollY + (float)(targetScroll - scrollY) * smoothFactor3);
				lastMouseY = GameCanvas.py;
				lastScrollY = scrollY;
			}
			scrollY = System.Math.Max(0, System.Math.Min(maxScroll, scrollY));
		}
		else if (!GameCanvas.isPointerDown)
		{
			isScrolling = false;
		}
		int num2 = scrollY / itemHeight;
		int endIndex = System.Math.Min(num2 + MAX_ITEMS_VISIBLE, listFilterItems.Count);
		for (int l = num2; l < endIndex; l++)
		{
			ItemAutoFilter item = listFilterItems[l];
			int itemY = panelY + 35 + l * itemHeight - scrollY;
			if (l % 2 == 0)
			{
				g.setColor(2105376, 0.3f);
				g.fillRect(panelX + 5, itemY, panelW - 10, itemHeight - 2, 5);
			}
			string info = item.name ?? "";
			mFont.tahoma_7_white.drawString(g, info, panelX + padding, itemY + 5, 0);
			mFont.tahoma_7_red.drawString(g, $"ID: {item.id}", panelX + padding, itemY + 15, 0);
			int delBtnW = 35;
			int delBtnH = 18;
			int delBtnX = panelX + panelW - delBtnW - padding;
			int delBtnY = itemY + 3;
			g.setColor(16724787);
			g.fillRect(delBtnX, delBtnY, delBtnW, delBtnH, 5);
			mFont.tahoma_7b_white.drawString(g, "Xóa", delBtnX + delBtnW / 2, delBtnY + 4, mFont.CENTER);
			if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				GameCanvas.isPointerJustDown = false;
				if (GameCanvas.isPointerHoldIn(delBtnX, delBtnY, delBtnW, delBtnH))
				{
					listFilterItems.RemoveAt(l);
					GameScr.info1.addInfo("Đã xóa vật phẩm khỏi danh sách lọc", 0);
					GameCanvas.clearAllPointerEvent();
				}
			}
		}
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		int scrollBarX = panelX + panelW - 8;
		int scrollBarY = panelY + 35;
		int scrollBarH = panelH - 45;
		int scrollBarW = 4;
		g.setColor(3355443);
		g.fillRect(scrollBarX, scrollBarY, scrollBarW, scrollBarH, 5);
		g.setColor(6710886);
		g.drawRect(scrollBarX, scrollBarY, scrollBarW, scrollBarH);
		if (contentHeight > visibleHeight)
		{
			float scrollRatio = (float)visibleHeight / (float)contentHeight;
			int scrollThumbH = (int)((float)scrollBarH * scrollRatio);
			int scrollThumbY = scrollBarY;
			if (scrollY > 0)
			{
				float scrollPercent = (float)scrollY / (float)maxScroll;
				scrollThumbY = scrollBarY + (int)((float)(scrollBarH - scrollThumbH) * scrollPercent);
			}
			scrollThumbY = System.Math.Max(scrollBarY, System.Math.Min(scrollBarY + scrollBarH - scrollThumbH, scrollThumbY));
			g.setColor(8947848);
			g.fillRect(scrollBarX + 1, scrollThumbY, scrollBarW - 2, scrollThumbH, 5);
			g.setColor(11184810);
			g.fillRect(scrollBarX + 1, scrollThumbY, scrollBarW - 2, 2, 5);
			g.setColor(6710886);
			g.fillRect(scrollBarX + 1, scrollThumbY + scrollThumbH - 2, scrollBarW - 2, 2, 5);
		}
		if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
		{
			GameCanvas.isPointerJustDown = false;
			if (GameCanvas.isPointerHoldIn(panelX + panelW - btnSize - 5, panelY + 5, btnSize, btnSize))
			{
				isShowFilterList = false;
				scrollY = 0;
				GameCanvas.clearAllPointerEvent();
			}
		}
	}

	public static void LoadSlThoiVangCanBan()
	{
		string text = Rms.loadRMSString(SlThoiVangCanBanRmsKey);
		if (!string.IsNullOrEmpty(text) && int.TryParse(text, out var result) && result > 0)
		{
			slThoiVangCanBan = result;
			return;
		}
		int num = Rms.loadRMSInt(SlThoiVangCanBanRmsKey);
		if (num > 0)
		{
			slThoiVangCanBan = num;
			SaveSlThoiVangCanBan();
			return;
		}
		slThoiVangCanBan = 1;
	}

	public static void SaveSlThoiVangCanBan()
	{
		Rms.saveRMSString(SlThoiVangCanBanRmsKey, slThoiVangCanBan.ToString());
	}

	public void UpdateModFuncText(int tabIndex)
	{
		if (tabIndex != 3)
		{
			return;
		}
		List<string> list = new List<string>(Panel.strModFunc);
		for (int i = list.Count - 1; i >= 0; i--)
		{
			if (list[i].Contains(strThoiVangBanMoiLan) || list[i].Contains("SL Th\u1ECFi V\u00E0ng C\u1EA7n B\u00E1n"))
			{
				list.RemoveAt(i);
			}
		}
		list.Add(strThoiVangBanMoiLan + ": " + slThoiVangCanBan);
		Panel.strModFunc = list.ToArray();
	}

	private void DoFilter()
	{
		if (!isAutoFilterItem)
		{
			return;
		}
		try
		{
			for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
			{
				Item item = Char.myCharz().arrItemBag[i];
				if (item == null)
				{
					continue;
				}
				foreach (ItemAutoFilter listFilterItem in listFilterItems)
				{
					if (listFilterItem.id == item.template.id)
					{
						Service.gI().useItem(1, 1, (sbyte)item.indexUI, -1);
						Thread.Sleep(50);
						return;
					}
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	public static void DoBoss()
	{
		if (string.IsNullOrEmpty(bossCanDo))
		{
			GameScr.info1.addInfo("Chưa nhập boss cần tìm", 0);
			zoneMacDinh = 0;
			isdoBoss = false;
			return;
		}
		if (Input.GetKey("q"))
		{
			GameScr.info1.addInfo("Đã tắt auto dò boss", 0);
			isdoBoss = false;
			return;
		}
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			Char @char = (Char)GameScr.vCharInMap.elementAt(i);
			if (@char != null && @char.cName.ToLower().Contains(bossCanDo.ToLower()) && @char.cTypePk == 5)
			{
				Sound.start(1f, Sound.l1);
				GameScr.info1.addInfo("Đã tìm thấy boss", 0);
				zoneMacDinh = 0;
				isdoBoss = false;
				return;
			}
		}
		if (GameScr.gI().numPlayer == null || GameScr.gI().numPlayer.Length == 0)
		{
			Service.gI().openUIZone();
			return;
		}
		Service.gI().requestChangeZone(zoneMacDinh, -1);
		if (!Char.isLoadingMap && TileMap.zoneID == zoneMacDinh)
		{
			zoneMacDinh++;
			if (zoneMacDinh >= GameScr.gI().numPlayer.Length)
			{
				zoneMacDinh = 0;
			}
		}
	}

	public static void LoadImgMenuChat()
	{
		imgMenuChat = GameCanvas.loadImage("/mainImage/MenuChat.png");
		imgCloseButton = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png");
		imgNextPage = GameCanvas.loadImage("/mainImage/myTexture2dbtnl.png");
		imgNextPage2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf.png");
		imgPrevPage = GameCanvas.loadImage("/mainImage/myTexture2dbtnl.png");
		imgPrevPage2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf.png");
	}

	public static void PaintMenuChat(mGraphics g)
	{
		if (!isShowMenuChat)
		{
			return;
		}
		int menuChatX = (GameCanvas.w - imgMenuChat.getWidth()) / 2;
		int menuChatY = (GameCanvas.h - imgMenuChat.getHeight()) / 2;
		g.drawImage(imgMenuChat, menuChatX, menuChatY);
		g.drawImage(imgCloseButton, menuChatX + imgMenuChat.getWidth() - imgCloseButton.getWidth(), menuChatY);
		string chatInfo = "Nhập lệnh chat tại đây:";
		mFont.tahoma_7b_red.drawString(g, chatInfo, menuChatX + 30, menuChatY + 10, mFont.LEFT);
		Dictionary<string, string> chatCommands = new Dictionary<string, string>
		{
			{ "htl", "Bật/tắt hành trang lưới" },
			{ "loadskill", "Tải lại ô skill" },
			{ "ak", "Bật/tắt tự động tấn công" },
			{ "ts", "Bật/tắt chế độ tàn sát" },
			{ "tsnguoi", "Bật/tắt chế độ tàn sát người" },
			{ "vqmm", "Bật/tắt tự động VQMM" },
			{ "ukhu", "Bật/tắt cập nhật khu tự động" },
			{ "k X", "Chuyển đến khu X (VD: k 5)" },
			{ "s X", "Thay đổi tốc độ game (1-10)" },
			{ "atc text", "Thiết lập tin nhắn tự động" },
			{ "atctg text", "Thiết lập tin nhắn tự động thế giới" },
			{ "do text", "Thiết lập boss cần dò" },
			{ "dbx", "Bật/tắt tự động dò boss" },
			{ "gtv", "Bật/tắt gõ Tiếng Việt" }
		};
		int totalPages = (int)System.Math.Ceiling((double)chatCommands.Count / 8.0);
		int num = currentPage * 8;
		int endIndex = System.Math.Min(num + 8, chatCommands.Count);
		int commandY = menuChatY + 30;
		for (int i = num; i < endIndex; i++)
		{
			KeyValuePair<string, string> command = chatCommands.ElementAt(i);
			string commandText = command.Key + ": " + command.Value;
			mFont.tahoma_7_yellow.drawStringBorder(g, commandText, menuChatX + 35, commandY, mFont.LEFT, mFont.tahoma_7_grey);
			commandY += 15;
		}
		if (currentPage > 0)
		{
			g.drawImage(imgPrevPage, menuChatX + 30, menuChatY + imgMenuChat.getHeight() - 30);
			mFont.tahoma_7b_white.drawString(g, "Trang trước", menuChatX + 40, menuChatY + imgMenuChat.getHeight() - 22, mFont.LEFT);
		}
		if (currentPage < totalPages - 1)
		{
			g.drawImage(imgNextPage, menuChatX + imgMenuChat.getWidth() - 100, menuChatY + imgMenuChat.getHeight() - 30);
			mFont.tahoma_7b_white.drawString(g, "Trang sau", menuChatX + imgMenuChat.getWidth() - 43, menuChatY + imgMenuChat.getHeight() - 22, mFont.RIGHT);
		}
		PaintLogoGifMenu(g, menuChatX + imgMenuChat.getWidth() - 150, menuChatY + (imgMenuChat.getHeight() - FrameGifMenu) / 2, mFont.CENTER);
		if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
		{
			if (GameCanvas.isPointerHoldIn(menuChatX + imgMenuChat.getWidth() - imgCloseButton.getWidth(), menuChatY, imgCloseButton.getWidth(), imgCloseButton.getHeight()))
			{
				isShowMenuChat = false;
				GameCanvas.clearAllPointerEvent();
			}
			if (currentPage > 0 && GameCanvas.isPointerHoldIn(menuChatX + 10, menuChatY + imgMenuChat.getHeight() - 30, 80, 20))
			{
				currentPage--;
			}
			if (currentPage < totalPages - 1 && GameCanvas.isPointerHoldIn(menuChatX + imgMenuChat.getWidth() - 80, menuChatY + imgMenuChat.getHeight() - 30, 80, 20))
			{
				currentPage++;
			}
		}
	}
}
