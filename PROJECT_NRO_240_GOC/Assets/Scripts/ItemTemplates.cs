public class ItemTemplates
{
	public static MyHashTable itemTemplates = new MyHashTable();

	public static void add(ItemTemplate it)
	{
		itemTemplates.put(it.id, it);
	}

	public static ItemTemplate get(short id)
	{
		ItemTemplate itemTemplate = (ItemTemplate)itemTemplates.get(id);
		if (itemTemplate == null)
		{
			itemTemplate = new ItemTemplate(id, 0, 0, "Vật phẩm [ID: " + id + "]", "Đang tải dữ liệu...", 0, 0, 0, -1, false);
			itemTemplates.put(id, itemTemplate);
		}
		return itemTemplate;
	}
}
