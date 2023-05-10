namespace BrewUp.Warehouse.ReadModel.DTOs

{
    public abstract class ModelBase : IEquatable<ModelBase>
    {
        public string Id { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ModelBase);
        }

        public bool Equals(ModelBase other)
        {
            return (null != other) && (GetType() == other.GetType()) && (other.Id == Id);
        }

        public static bool operator ==(ModelBase Dto1, ModelBase Dto2)
        {
            if ((object)Dto1 == null && (object)Dto2 == null)
                return true;
            if ((object)Dto1 == null || (object)Dto2 == null)
                return false;
            return ((Dto1.GetType() == Dto2.GetType()) && (Dto1.Id == Dto2.Id));
        }

        public static bool operator !=(ModelBase Dto1, ModelBase Dto2)
        {
            return (!(Dto1 == Dto2));
        }
    }
}