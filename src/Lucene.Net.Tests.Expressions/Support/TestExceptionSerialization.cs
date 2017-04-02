﻿#if FEATURE_SERIALIZABLE
using Lucene.Net.Attributes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lucene.Net.Support
{
    /*
     * Licensed to the Apache Software Foundation (ASF) under one or more
     * contributor license agreements.  See the NOTICE file distributed with
     * this work for additional information regarding copyright ownership.
     * The ASF licenses this file to You under the Apache License, Version 2.0
     * (the "License"); you may not use this file except in compliance with
     * the License.  You may obtain a copy of the License at
     *
     *     http://www.apache.org/licenses/LICENSE-2.0
     *
     * Unless required by applicable law or agreed to in writing, software
     * distributed under the License is distributed on an "AS IS" BASIS,
     * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     * See the License for the specific language governing permissions and
     * limitations under the License.
     */

    [TestFixture]
    public class TestExceptionSerialization : ExceptionSerializationTestBase
    {
        public static IEnumerable<object> ExceptionTestData
        {
            get
            {
                var exceptionTypes = typeof(Lucene.Net.Expressions.Bindings).Assembly.GetTypes().Where(t => typeof(Exception).IsAssignableFrom(t)).Cast<object>();

                // If the assembly has no exceptions, just provide Exception so the test will pass
                if (!exceptionTypes.Any())
                {
                    return new Type[] { typeof(Exception) };
                }

                return exceptionTypes;
            }
        }

        [Test, LuceneNetSpecific]
        public void AllExceptionsInLuceneNamespaceCanSerialize([ValueSource("ExceptionTestData")]Type luceneException)
        {
            var instance = TryInstantiate(luceneException);
            Assert.That(TypeCanSerialize(instance), string.Format("Unable to serialize {0}", luceneException.FullName));
        }
    }
}
#endif