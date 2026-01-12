const API_URL = "http://localhost:5139/api/AutoPecas";

const btnCarregar = document.getElementById("btnCarregar");
const lista = document.getElementById("listaPecas");

btnCarregar.addEventListener("click", carregarPecas);

function carregarPecas() {
    fetch(API_URL)
        .then(response => response.json())
        .then(response => {

            // 🔹 Validação padrão da API
            if (!response.success) {
                throw new Error(response.message);
            }

            lista.innerHTML = "";

            response.data.forEach(peca => {
                const li = document.createElement("li");
                li.textContent = `Nome da peça: ${peca.nome} -- Marca da peça: ${peca.marca} -- Preço da peça R$ ${peca.preco}`;
                lista.appendChild(li);
            });
        })
        .catch(error => {
            console.error("Erro ao buscar peças:", error.message);
            alert(error.message || "Erro ao carregar peças");
        });
}
