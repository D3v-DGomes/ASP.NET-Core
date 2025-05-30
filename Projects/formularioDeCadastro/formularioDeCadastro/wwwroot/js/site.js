// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", () => {
    const btnSalvar = document.getElementById("btnSalvar");
    const cadastroForm = document.getElementById("cadastroForm");
    const mensagemDiv = document.getElementById("mensagemDiv");

    btnSalvar.addEventListener("click", async () => {
        const nome = document.getElementById("nome").value;
        const email = document.getElementById("email").value;
        const senha = document.getElementById("senha").value;

        const userData = {
            nome: nome,
            email: email,
            senha: senha
        };

        try {
            const response = await fetch("/Usuarios/Cadastrar", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(userData)
            });
            
            response.json().then(data => {
                if (response.ok) {
                    mensagemDiv.className = 'mensagem sucesso';
                    mensagemDiv.textContent = data.message || 'Cadastro realizado com sucesso!';
                    cadastroForm.reset();
                }
                else {
                    mensagemDiv.className = 'mensagem erro';
                    mensagemDiv.textContent = data.error || 'Erro ao cadastrar.';
                }
            });
        }
        catch (error) {
            console.error("Erro ao enviar dados:", error);
            mensagemDiv.className = "mensagem erro";
            mensagemDiv.textContent = "Erro ao enviar dados. Por favor, tente novamente mais tarde.";
        }
    })
});