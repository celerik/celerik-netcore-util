namespace Celerik.NetCore.Util
{
    /// <summary>
    /// Defines the possible strategies when updating an entity in
    /// a Table Storage.
    /// </summary>
    public enum TableStorageStrategy
    {
        /// <summary>
        /// The entity will be inserted if not exists, otherwise
        /// the properties of the updating entity will be merged
        /// into the properties of the existing one.
        /// </summary>
        InsertOrMerge = 1,

        /// <summary>
        /// The entity will be inserted if not exists, otherwise
        /// the existing entity will be completely replaced by the
        /// updating entity.
        /// </summary>
        InsertOrReplace = 2,

        /// <summary>
        /// The properties of the updating entity will be merged
        /// into the properties of the existing one.
        /// </summary>
        Merge = 3,

        /// <summary>
        /// The existing entity will be completely replaced by the
        /// updating entity.
        /// </summary>
        Replace = 4
    }
}
