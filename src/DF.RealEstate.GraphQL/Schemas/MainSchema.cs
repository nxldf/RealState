using Abp.Dependency;
using GraphQL.Types;
using GraphQL.Utilities;
using DF.RealEstate.Queries.Container;
using System;

namespace DF.RealEstate.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IServiceProvider provider) :
            base(provider)
        {
            Query = provider.GetRequiredService<QueryContainer>();
        }
    }
}