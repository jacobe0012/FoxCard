using MessagePack;


[MessagePackObject]
public class PlayerData
{
    [Key(0)] public long Id { get; set; }
        
    
    [Key(1)] public string NickName { get; set; }
    
    [Key(2)] public string LastName { get; set; }
    
    
}