namespace Sokudo.Api.ViewModels
{
    using System.Collections.Generic;
    using Sokudo.Api.ViewModelSchemaFilters;
    using Swashbuckle.AspNetCore.SwaggerGen;

    [SwaggerSchemaFilter(typeof(PageResultCarSchemaFilter))]
    public class PageResult<T>
        where T : class
    {
        public int Page { get; set; }

        public int Count { get; set; }

        public bool HasNextPage { get => this.Page < this.Total; }

        public bool HasPreviousPage { get => this.Page > 1; }

        public int Total { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
