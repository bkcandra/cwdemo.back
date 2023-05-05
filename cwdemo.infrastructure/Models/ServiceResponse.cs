using System.Text.Json.Serialization;

namespace cwdemo.infrastructure.Models
{
    /// <summary>
    /// Return type when invoking a service method that indicates success / failure and a descriptive string
    /// </summary>
    public class ServiceResponse
    {
        /// <summary>
        /// Create a service response with the indicated success state
        /// </summary>
        /// <param name="success"></param>
        public ServiceResponse(bool valid)
        {
            Valid = valid;
            Message = new List<string>();
        }

        /// <summary>
        /// Create a service response with a failed state
        /// </summary>
        public ServiceResponse() : this(true)
        {

        }

        /// <summary>
        /// Create a service response  with the indicated success state and descriptive string
        /// </summary>
        /// <param name="success">The success state</param>
        /// <param name="message">The descriptive string</param>
        public ServiceResponse(bool valid, string message) : this(valid)
        {
            if (!string.IsNullOrEmpty(message))
                Message.Add(message);
        }

        /// <summary>
        /// Gets the success status for the service method
        /// </summary>
        /// <remarks>Default value of <b>false</b></remarks>
        public bool Valid { get; set; } = false;

        /// <summary>
        /// Gets the descriptive string provided by the service method
        /// </summary>
        /// <remarks>Default value of null</remarks>
        public List<string> Message { get; set; } = null;

        /// <summary>
        /// Get the Http Status Code to returned to client
        /// </summary>
        [JsonIgnore]
        public int StatusCode { get; set; }

        /// <summary>
        /// Unique identifier to represent this request in trace logs.
        /// </summary>
        public string CorrelationToken { get; set; }
    }

    /// <summary>
    /// Return type when invoking a service method that indicates success / failure, descriptive string, and a strongly typed data payload
    /// </summary>
    public class ServiceResponse<T> : ServiceResponse
    {
        public ServiceResponse() : this(true)
        { }

        /// <summary>
        /// Create a service response with the indicated success state
        /// </summary>
        /// <param name="success">The success state</param>
        public ServiceResponse(bool success)
            : this(success, string.Empty)
        { }

        /// <summary>
        /// Create a service response with the indicated success state and descriptive string
        /// </summary>
        /// <param name="success">The success state</param>
        /// <param name="message">the descriptive string</param>
        public ServiceResponse(bool success, string message)
            : base(success, message)
        {
            if (typeof(T).IsValueType || typeof(T) == typeof(String))
            {
                this.Content = default(T);
            }
            else
            {
                this.Content = (T)Activator.CreateInstance(typeof(T));
            }
        }
        /// <summary>
        /// Create a service response with the indicated success state and descriptive string
        /// </summary>
        /// <param name="success">The success state</param>
        /// <param name="message">the descriptive string</param>
        public ServiceResponse(bool success, string message, int statusCode)
            : base(success, message)
        {
            if (typeof(T).IsValueType || typeof(T) == typeof(String))
            {
                this.Content = default(T);
            }
            else
            {
                this.Content = (T)Activator.CreateInstance(typeof(T));
            }

            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Create a successful service response with a data payload
        /// </summary>
        /// <param name="data">The data payload</param>
        public ServiceResponse(T data)
            : base(true)
        {
            Content = data;
        }

        /// <summary>
        /// Create a successful service response with data payload and descriptive string
        /// </summary>
        /// <param name="data">The data payload</param>
        /// <param name="message">The descriptive string</param>
        public ServiceResponse(T data, string message)
            : base(true, message)
        {
            Content = data;
        }

        /// <summary>
        /// Gets the data payload generated on success of the service method
        /// </summary>
        public T Content { get; set; }
    }
}
