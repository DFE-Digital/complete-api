﻿namespace Dfe.Complete.Infrastructure.Gateways;
public class ApiV2PagingInfo
{
   public int Page { get; set; }
   public int RecordCount { get; set; }
   public string? NextPageUrl { get; set; }
}
