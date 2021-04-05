using Prometheus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Prometheus.Business.Reflection
{
    public class CatchJobMethods
    {
        public IEnumerable<JobMethodsList> GetRuntimeJobMethods()
        {
            List<JobMethodsList> list = new List<JobMethodsList>();
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in a.GetTypes())
                {
                    foreach (var m in t.GetMembers())
                    {
                        if (Attribute.IsDefined(m, typeof(JobMethod)))
                        {
                            var method = new JobMethodsList()
                            {
                                Id = m.MetadataToken,
                                MethodName = m.Name,
                                TypeName = t.Name
                            };

                            list.Add(method);
                        }
                    }
                }
            }

            return list;
        }
    }
}
