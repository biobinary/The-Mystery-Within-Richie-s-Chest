using System;

public static class PlayerKeyInventory
{
   
    private static int m_currentKey = -1;
    public static Action<ChestKey.TYPE> onNewKeyAdded;
    public static Action<ChestKey.TYPE> onRemoveOldKey;

    public static bool HasKey(ChestKey.TYPE type) {
        if (!IsHaveKey()) return false;
        return ((int)type) == m_currentKey;
    }

    public static void AddKey(ChestKey.TYPE type) {
        m_currentKey = (int)type;
        onNewKeyAdded?.Invoke(type);
    }

    public static void RemoveKey() {
        onRemoveOldKey?.Invoke((ChestKey.TYPE)m_currentKey);
        m_currentKey = -1;
    }

    public static bool IsHaveKey() {
        return m_currentKey != -1; 
    }

}
