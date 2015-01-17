// -----------------------------------------------------------------------
// <copyright file="ODataException.cs" company="Project Contributors">
// Copyright 2012 - 2015 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception which is thrown if there is an error processing an OData query.
    /// </summary>
    [Serializable]
    public class ODataException : InvalidOperationException
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ODataException"/> class.
        /// </summary>
        public ODataException()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ODataException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception.</param>
        public ODataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ODataException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        protected ODataException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}