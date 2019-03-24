namespace MrRondon.Pages
{
    public class PermissionDeniedPageModel : BasePageModel
    {
        public string Message { get; set; }

        public PermissionDeniedPageModel(string title, params string[] permissoes)
        {
            Title = title;
            Message = $"Você precisa de habilitar as seguintes permissões: {string.Join(", ", permissoes)}";
        }
    }
}