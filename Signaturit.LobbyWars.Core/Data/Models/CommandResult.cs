namespace Signaturit.LobbyWars.Core.Data.Models
{
    public class CommandResult
    {
        public bool IsSuccess { get; private set; }

        public string Identity { get; private set; }

        public string Error { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="identity"></param>
        /// <param name="errorMessage"></param>
        public CommandResult(bool isSuccess, object identity, string errorMessage)
        {
            IsSuccess = isSuccess;
            Identity = $"{identity}";
            Error = errorMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="identity"></param>
        public CommandResult(bool isSuccess, object identity)
            : this(isSuccess, identity, string.Empty)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        public CommandResult(string errorMessage)
            : this(isSuccess: false, identity: string.Empty, errorMessage)
        { }

        /// <summary>
        /// 
        /// </summary>
        public CommandResult()
        {
            Identity = string.Empty;
            Error = string.Empty;
            IsSuccess = true;
        }
    }

    public class CommandResult<T> : CommandResult
    {
        public T Item { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="identity"></param>
        /// <param name="errorMessage"></param>
        /// <param name="item"></param>
        public CommandResult(bool isSuccess, object identity, string errorMessage, T item)
            : base(isSuccess, identity, errorMessage)
        {
            Item = item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="identity"></param>
        /// <param name="item"></param>
        public CommandResult(bool isSuccess, object identity, T item)
            : this(isSuccess, identity, string.Empty, item)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="item"></param>
        public CommandResult(string errorMessage, T item)
            : this(isSuccess: false, identity: string.Empty, errorMessage, item)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public CommandResult(T item)
            : this(isSuccess: true, identity: string.Empty, string.Empty, item)
        { }

        /// <summary>
        /// 
        /// </summary>
        public CommandResult()
            : this(isSuccess: false, identity: string.Empty, string.Empty, item: default)
        { }
    }
}