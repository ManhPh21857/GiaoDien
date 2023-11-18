using Presentation.Core.Domain;
using Presentation.Core.Domain.Authentication;

namespace Presentation.Core.Service;

public class SessionInfo : ISessionInfo {
    public SessionInfo() {
        Token = new Token(string.Empty);
    }

    public void Initialize(Token token) {
        Token = token;
    }

    public Token Token { get; set; }
}