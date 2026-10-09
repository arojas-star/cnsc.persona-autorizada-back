namespace personaAutorizadaBack.Dtos
{
    public sealed record VessperienciasAreaResponse(
        int codigoNivel,
        string Descripcion
    );

    public sealed record VessperienciasPersonalResponse(
        int CodigoPersonal,
        string Descripcion,
        int? CodigoArea
    );

    public sealed record VessperienciasTemaResponse(
        int CodigoTema,
        string Descripcion
    );
}
