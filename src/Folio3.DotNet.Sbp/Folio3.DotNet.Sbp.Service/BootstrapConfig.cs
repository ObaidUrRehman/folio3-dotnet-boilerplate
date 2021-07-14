﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Folio3.DotNet.Sbp.Service
{
	public static class BootstrapConfig
	{
		public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
		{
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MappingProfile());
			});

			return services
				.AddSingleton(mappingConfig.CreateMapper())
				;
		}
	}
}
