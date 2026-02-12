using Unity.Entities;

public struct DuplicateBeamData : IComponentData 
{
    public float MovingSpeed;
    public int duplicationAmount;
}