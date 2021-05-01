using Microsoft.Extensions.DependencyInjection;
using ServicioAPISeguridad.Application.Interfaces;
using ServicioAPISeguridad.Application.Main;
using ServicioAPISeguridad.Domain.Interfaces;
using ServicioAPISeguridad.Domain.Main;
using ServicioAPISeguridad.Infraestructure.Configuration;
using ServicioAPISeguridad.Infraestructure.Interfaces;
using ServicioAPISeguridad.Infraestructure.Repository;
using System;

namespace ServicioAPISeguridad.Transversal.IoC
{
    public static class IoC
    {
        public static IServiceCollection AddRegistration(this IServiceCollection service)
        {

            service.AddSingleton<IConnectionFactory, ConnectionFactory>();

            service.AddSingleton<IUsuarioRepository, UsuarioRepository>();
            service.AddSingleton<IUsuarioDomain, UsuarioDomain>();
            service.AddSingleton<IUsuarioApplication, UsuarioApplication>();

            return service;
        }
    }
}
