using Presentation.Core.Domain.Authentication;

namespace Presentation.Core.Domain; 

public interface ISessionInfo {
    public Token Token { get; set; }
}