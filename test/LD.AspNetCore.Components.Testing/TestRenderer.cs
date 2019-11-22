using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace LD.AspNetCore.Components.Testing
{
    [SuppressMessage("Usage", "BL0006:Do not use RenderTree types", Justification = "<Pending>")]
    internal class TestRenderer : Renderer
    {
        public TestRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory) 
            : base(serviceProvider, loggerFactory)
        {
        }

        public override Dispatcher Dispatcher => Dispatcher.CreateDefault();

        protected override void HandleException(Exception exception)
        {
            throw new NotImplementedException();
        }

        protected override Task UpdateDisplayAsync(in RenderBatch renderBatch)
        {
            throw new NotImplementedException();
        }
    }
}
