﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookServiceReference
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BookResponseDtoEF", Namespace="http://schemas.datacontract.org/2004/07/WcfService.Dto.Book")]
    public partial class BookResponseDtoEF : object
    {
        
        private string CodeField;
        
        private System.DateTime CreatedAtField;
        
        private int IdBookField;
        
        private System.Nullable<bool> IsAvailableField;
        
        private System.Nullable<int> StatusField;
        
        private string TitleField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Code
        {
            get
            {
                return this.CodeField;
            }
            set
            {
                this.CodeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreatedAt
        {
            get
            {
                return this.CreatedAtField;
            }
            set
            {
                this.CreatedAtField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdBook
        {
            get
            {
                return this.IdBookField;
            }
            set
            {
                this.IdBookField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> IsAvailable
        {
            get
            {
                return this.IsAvailableField;
            }
            set
            {
                this.IsAvailableField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Status
        {
            get
            {
                return this.StatusField;
            }
            set
            {
                this.StatusField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title
        {
            get
            {
                return this.TitleField;
            }
            set
            {
                this.TitleField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BookResponseDto", Namespace="http://schemas.datacontract.org/2004/07/WcfService.Dto.Book")]
    public partial class BookResponseDto : object
    {
        
        private string CodeField;
        
        private System.DateTime CreatedAtField;
        
        private System.Nullable<System.DateTime> DateReservationField;
        
        private int IdBookField;
        
        private System.Nullable<bool> IsAvailableField;
        
        private System.Nullable<int> StatusField;
        
        private string TitleField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Code
        {
            get
            {
                return this.CodeField;
            }
            set
            {
                this.CodeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreatedAt
        {
            get
            {
                return this.CreatedAtField;
            }
            set
            {
                this.CreatedAtField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateReservation
        {
            get
            {
                return this.DateReservationField;
            }
            set
            {
                this.DateReservationField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdBook
        {
            get
            {
                return this.IdBookField;
            }
            set
            {
                this.IdBookField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> IsAvailable
        {
            get
            {
                return this.IsAvailableField;
            }
            set
            {
                this.IsAvailableField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Status
        {
            get
            {
                return this.StatusField;
            }
            set
            {
                this.StatusField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title
        {
            get
            {
                return this.TitleField;
            }
            set
            {
                this.TitleField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ReservationRequest", Namespace="http://schemas.datacontract.org/2004/07/WcfService.Dto.Reservation")]
    public partial class ReservationRequest : object
    {
        
        private System.Nullable<System.DateTime> DateReservationField;
        
        private int IdBookField;
        
        private int IdUserField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateReservation
        {
            get
            {
                return this.DateReservationField;
            }
            set
            {
                this.DateReservationField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdBook
        {
            get
            {
                return this.IdBookField;
            }
            set
            {
                this.IdBookField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdUser
        {
            get
            {
                return this.IdUserField;
            }
            set
            {
                this.IdUserField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ReservationResponse", Namespace="http://schemas.datacontract.org/2004/07/WcfService.Dto.Reservation")]
    public partial class ReservationResponse : object
    {
        
        private string BookNameField;
        
        private System.DateTime CreatedAtField;
        
        private System.Nullable<System.DateTime> DateReservationField;
        
        private int IdBookField;
        
        private int IdResevationField;
        
        private int IdUserField;
        
        private System.Nullable<int> StatusField;
        
        private string UserNameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BookName
        {
            get
            {
                return this.BookNameField;
            }
            set
            {
                this.BookNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreatedAt
        {
            get
            {
                return this.CreatedAtField;
            }
            set
            {
                this.CreatedAtField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateReservation
        {
            get
            {
                return this.DateReservationField;
            }
            set
            {
                this.DateReservationField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdBook
        {
            get
            {
                return this.IdBookField;
            }
            set
            {
                this.IdBookField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdResevation
        {
            get
            {
                return this.IdResevationField;
            }
            set
            {
                this.IdResevationField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdUser
        {
            get
            {
                return this.IdUserField;
            }
            set
            {
                this.IdUserField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Status
        {
            get
            {
                return this.StatusField;
            }
            set
            {
                this.StatusField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName
        {
            get
            {
                return this.UserNameField;
            }
            set
            {
                this.UserNameField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BookServiceReference.IBookService")]
    public interface IBookService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookService/GetAllEF", ReplyAction="http://tempuri.org/IBookService/GetAllEFResponse")]
        System.Threading.Tasks.Task<BookServiceReference.BookResponseDtoEF[]> GetAllEFAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookService/GetAll", ReplyAction="http://tempuri.org/IBookService/GetAllResponse")]
        System.Threading.Tasks.Task<BookServiceReference.BookResponseDto[]> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookService/GetSearch", ReplyAction="http://tempuri.org/IBookService/GetSearchResponse")]
        System.Threading.Tasks.Task<BookServiceReference.BookResponseDto[]> GetSearchAsync(string search);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookService/GetById", ReplyAction="http://tempuri.org/IBookService/GetByIdResponse")]
        System.Threading.Tasks.Task<BookServiceReference.BookResponseDto> GetByIdAsync(int idBook);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBookService/CreateReservation", ReplyAction="http://tempuri.org/IBookService/CreateReservationResponse")]
        System.Threading.Tasks.Task<BookServiceReference.ReservationResponse> CreateReservationAsync(BookServiceReference.ReservationRequest reservation);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public interface IBookServiceChannel : BookServiceReference.IBookService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public partial class BookServiceClient : System.ServiceModel.ClientBase<BookServiceReference.IBookService>, BookServiceReference.IBookService
    {
        
        /// <summary>
        /// Implemente este método parcial para configurar el punto de conexión de servicio.
        /// </summary>
        /// <param name="serviceEndpoint">El punto de conexión para configurar</param>
        /// <param name="clientCredentials">Credenciales de cliente</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public BookServiceClient() : 
                base(BookServiceClient.GetDefaultBinding(), BookServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IBookService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public BookServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(BookServiceClient.GetBindingForEndpoint(endpointConfiguration), BookServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public BookServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(BookServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public BookServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(BookServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public BookServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<BookServiceReference.BookResponseDtoEF[]> GetAllEFAsync()
        {
            return base.Channel.GetAllEFAsync();
        }
        
        public System.Threading.Tasks.Task<BookServiceReference.BookResponseDto[]> GetAllAsync()
        {
            return base.Channel.GetAllAsync();
        }
        
        public System.Threading.Tasks.Task<BookServiceReference.BookResponseDto[]> GetSearchAsync(string search)
        {
            return base.Channel.GetSearchAsync(search);
        }
        
        public System.Threading.Tasks.Task<BookServiceReference.BookResponseDto> GetByIdAsync(int idBook)
        {
            return base.Channel.GetByIdAsync(idBook);
        }
        
        public System.Threading.Tasks.Task<BookServiceReference.ReservationResponse> CreateReservationAsync(BookServiceReference.ReservationRequest reservation)
        {
            return base.Channel.CreateReservationAsync(reservation);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IBookService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IBookService))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:53057/Services/Book/BookService.svc");
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return BookServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IBookService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return BookServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IBookService);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IBookService,
        }
    }
}
