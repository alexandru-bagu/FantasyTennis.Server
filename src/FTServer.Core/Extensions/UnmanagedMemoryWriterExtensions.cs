using FTServer.Contracts.MemoryManagement;
using FTServer.Database.Model;
using System;

public static class UnmanagedMemoryWriterExtensions
{
    public static IUnmanagedMemoryWriter Write(this IUnmanagedMemoryWriter writer, Item item)
    {
        return writer.WriteItem(item);
    }
    public static IUnmanagedMemoryWriter WriteItem(this IUnmanagedMemoryWriter writer, Item item)
    {
        writer.WriteInt32(item.Id);
        writer.WriteByte(item.CategoryType);
        writer.WriteInt32(item.Index);
        writer.WriteByte(item.UseType);
        writer.WriteInt32(item.Quantity);
        if (item.ExpirationDate == null)
            writer.WriteInt64(0);
        else
            writer.WriteInt64((long)((item.ExpirationDate.Value - DateTime.Now).TotalMilliseconds * 10000));
        writer.WriteByte(item.EnchantStrength);
        writer.WriteByte(item.EnchantStamina);
        writer.WriteByte(item.EnchantDexterity);
        writer.WriteByte(item.EnchantWillpower);
        writer.WriteByte(item.Unknown1);
        writer.WriteByte(item.Unknown2);
        return writer;
    }
}
