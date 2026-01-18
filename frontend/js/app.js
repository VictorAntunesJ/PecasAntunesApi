// ===============================
// CONFIG
// ===============================
const API_URL = "http://localhost:5139/api/AutoPecas";

// ===============================
// ELEMENTOS
// ===============================
const btnCarregar = document.getElementById("btnCarregar");
const lista = document.getElementById("listaPecas");

const formPeca = document.getElementById("formPeca");
const mensagem = document.getElementById("mensagem");

// ===============================
// EVENTOS
// ===============================
if (btnCarregar) {
    btnCarregar.addEventListener("click", carregarPecas);
}

if (formPeca) {
    formPeca.addEventListener("submit", cadastrarPeca);
}

// ===============================
// GET - LISTAR PEÇAS
// ===============================
function carregarPecas() {
    fetch(API_URL)
        .then(response => response.json())
        .then(response => {

            // 🔹 valida response padrão
            if (!response.success) {
                throw new Error(response.message);
            }

            lista.innerHTML = "";

            response.data.forEach(peca => {
                const li = document.createElement("li");
                li.textContent =
                    `Nome: ${peca.nome} | Marca: ${peca.marca} | R$ ${peca.preco}`;
                lista.appendChild(li);
            });
        })
        .catch(error => {
            console.error("Erro ao buscar peças:", error.message);
            alert(error.message);
        });
}

// ===============================
// POST - CADASTRAR PEÇA
// ===============================
function cadastrarPeca(event) {
    event.preventDefault();

    const payload = {
        nome: document.getElementById("nome").value,
        codigo: "CX-001", // 🔴 obrigatório pelo domínio
        marca: document.getElementById("marca").value,
        preco: Number(document.getElementById("preco").value),
        quantidadeEstoque: 10, // 🔴 obrigatório
        descricao: "Cadastro via front-end"
    };

    fetch(API_URL, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(payload)
    })
        .then(response => response.json())
        .then(response => {

            // 🔹 valida response padrão
            if (!response.success) {
                throw new Error(response.message);
            }

            mensagem.textContent = response.message;
            mensagem.style.color = "green";

            formPeca.reset();

            // 🔹 recarrega lista automaticamente
            carregarPecas();
        })
        .catch(error => {
            console.error("Erro ao cadastrar:", error.message);
            mensagem.textContent = error.message;
            mensagem.style.color = "red";
        });
}
