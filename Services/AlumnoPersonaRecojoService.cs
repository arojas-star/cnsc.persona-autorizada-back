using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;
using personaAutorizadaBack.Repository.Interfaces;
using personaAutorizadaBack.Services.Interfaces;

namespace personaAutorizadaBack.Services
{
    public class AlumnoPersonaRecojoService : IAlumnoPersonaRecojoService
    {
        private readonly IAlumnoPersonaRecojoRepository _personRecojoRepository;
        private readonly IAlumnoRepository _alumnoRepository;

        public AlumnoPersonaRecojoService(IAlumnoPersonaRecojoRepository personRecojoRepository, IAlumnoRepository alumnoRepository)
        {
            _personRecojoRepository = personRecojoRepository;
            _alumnoRepository = alumnoRepository;
        }

        public async Task<ServiceResult<AlumnoPersonaRecojoDto>> RegistrarPersonaRecojo(AlumnoPersonaRecojoDto personaRecojoDto)
        {
            var existe = await _alumnoRepository.ExisteAlumnoPorCodigo(personaRecojoDto.CodigoAlumno);

            if (existe == null)
            {
                return ServiceResult<AlumnoPersonaRecojoDto>.Fail(
                    $"Ah ocurrido un error al encontrar alumno con el codigo: {personaRecojoDto.CodigoAlumno}",
                    404
                );
            }

            /*
                * Ya no se va a bloquear cuando se haya registrado con anterioridad para un mismo alumno/estudiante
             */
            //var tieneRegistros = await _personRecojoRepository.ExisteRegistroXAlumno(personaRecojoDto.CodigoAlumno);

            //if (tieneRegistros.Count() > 0)
            //{
            //    return ServiceResult<AlumnoPersonaRecojoDto>.Fail(
            //        "Ya existen registros previos para este estudiante",
            //        409
            //    );
            //}
            /*
               **********
            */

            List<AlumnoResumenDto> alumnos = [existe];

            if (personaRecojoDto.codigoAlumnosParientes != null && personaRecojoDto.codigoAlumnosParientes.Count > 0)
            {
                foreach (var cod in personaRecojoDto.codigoAlumnosParientes)
                {
                    var resultado = await _alumnoRepository
                        .ExisteAlumnoPorCodigo(cod);

                    if (resultado is null)
                    {
                        continue;
                    }

                    alumnos.Add(resultado);
                }
            }

            List<AlumnoPersonaRecojo> alumnoPersonasRecojoData = [];

            foreach (var personaRecojo in personaRecojoDto.personaRecojo)
            {
                foreach(var alumno in alumnos)
                {
                    var alumnoPersonaRecojoData = new AlumnoPersonaRecojo
                    {
                        CodigoAlumno = alumno.CodigoAlumno,
                        ApellidoMaterno = personaRecojo.ApellidoMaterno,
                        ApellidoPaterno = personaRecojo.ApellidoPaterno,
                        CodigoTipoDocumento = personaRecojo.TipoDocumento,
                        NumeroDocumento = personaRecojo.NumeroDocumento,
                        Nombres = personaRecojo.Nombres,
                    };

                    var existeRegistroDuplidado = await _personRecojoRepository.ExisteRegistroXAlumnoPersonaAutorizada(alumno.CodigoAlumno, personaRecojo.TipoDocumento, personaRecojo.NumeroDocumento);

                    if (existeRegistroDuplidado is null)
                    {
                        alumnoPersonasRecojoData.Add(alumnoPersonaRecojoData);
                    }
                }
            }

            await _personRecojoRepository.RegistrarPersonasRecojo(alumnoPersonasRecojoData);
            return ServiceResult<AlumnoPersonaRecojoDto>.Ok(
                personaRecojoDto
            );

        }
    }
}
