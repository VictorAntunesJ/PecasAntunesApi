console.log("APP.JS CARREGADO");

// ===============================
// CONFIG
// ===============================
const API_URL = "https://pecasantunes-api.onrender.com/api/v1/AutoPecas";

// ===============================
// ELEMENTOS
// ===============================
const btnCarregar = document.getElementById("btnCarregar");
const lista = document.getElementById("listaPecas");

const formPeca = document.getElementById("formPeca");
const mensagem = document.getElementById("mensagem");
let pecaEditandoId = null;

const inputCodigo = document.getElementById("codigo");
const inputNome = document.getElementById("nome");
const inputMarca = document.getElementById("marca");
const inputPreco = document.getElementById("preco");
const inputQuantidadeEstoque = document.getElementById("quantidadeEstoque");
const inputDescricao = document.getElementById("descricao");

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
    .then((response) => response.json())
    .then((response) => {
      // 🔹 valida response padrão
      if (!response.success) {
        throw new Error(response.message);
      }

      lista.innerHTML = "";

      response.data.forEach((peca) => {
        const li = document.createElement("li");
        li.textContent = `Código: ${peca.codigo} | 
                          Nome: ${peca.nome} | 
                          Marca: ${peca.marca} | 
                          R$ ${peca.preco} | 
                          Qtde: ${peca.quantidadeEstoque} | 
                          Descrição: ${peca.descricao}`;

        const btnEditar = document.createElement("button");
        btnEditar.textContent = "Editar";

        btnEditar.onclick = () => {
          console.log("Editar peça ID:", peca.id);
          mostrarDetalhes(peca.id);
        };

        const btnExcluir = document.createElement("button");
        btnExcluir.textContent = "Excluir";
        btnExcluir.style.marginLeft = "10px";

        btnExcluir.onclick = () => {
          confirmarExclusao(peca.id, peca.codigo, peca.nome);
        };

        li.appendChild(btnExcluir);

        li.appendChild(btnEditar);

        lista.appendChild(li);
      });
    })
    .catch((error) => {
      console.error("Erro ao buscar peças:", error.message);
      alert(error.message);
    });
}

// ===============================
// POST - CADASTRAR PEÇA
// ===============================
function cadastrarPeca(event) {
  event.preventDefault();
  console.log("Modo de edição ID:", pecaEditandoId);

  const payload = {
    id: pecaEditandoId,
    nome: inputNome.value,
    codigo: inputCodigo.value,
    marca: inputMarca.value,
    preco: Number(inputPreco.value),
    quantidadeEstoque: Number(inputQuantidadeEstoque.value),
    descricao: inputDescricao.value,
  };

  console.log("PAYLOAD ENCIADO:", payload);

  // 🔥 INTELIGÊNCIA
  const metodo = pecaEditandoId ? "PUT" : "POST";
  const url = pecaEditandoId ? `${API_URL}/${pecaEditandoId}` : API_URL;

  fetch(url, {
    method: metodo,
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  })
    .then((response) => response.json())
    .then((response) => {
      if (!response.success) {
        throw new Error(response.message);
      }

      mensagem.textContent = response.message;
      mensagem.style.color = "green";

      formPeca.reset();

      // 🔹 IMPORTANTE: sai do modo edição
      pecaEditandoId = null;

      carregarPecas();
    })
    .catch((error) => {
      console.error("Erro ao salvar:", error.message);
      mensagem.textContent = error.message;
      mensagem.style.color = "red";
    });
}

//=================================
// EDITAR -  TESTE DE CLIQUE
//=================================
function mostrarDetalhes(id) {
  console.log("Editar peça ID:", id);

  // 🔹 Guarda o ID corretamente
  pecaEditandoId = id;

  console.log("Buscando peça ID:", pecaEditandoId);

  fetch(`${API_URL}/${pecaEditandoId}`)
    .then((response) => response.json())
    .then((response) => {
      if (!response.success) {
        throw new Error(response.message);
      }

      console.log("Peça encontrada:", response.data);

      inputCodigo.value = response.data.codigo;
      inputNome.value = response.data.nome;
      inputMarca.value = response.data.marca;
      inputPreco.value = response.data.preco;
      inputQuantidadeEstoque.value = response.data.quantidadeEstoque;
      inputDescricao.value = response.data.descricao;
    })
    .catch((error) => {
      console.error("Erro ao buscar peça:", error.message);
    });
}

function confirmarExclusao(id, codigo, nome) {
  const ok = confirm(
    `Tem certeza que deseja excluir a peça?\n\n` +
      `ID: ${id}\n` +
      `Código: ${codigo}\n` +
      `Nome: ${nome}`,
  );

  if (!ok) return;

  excluirPeca(id, codigo, nome);
}

function excluirPeca(id, codigo, nome) {
    fetch(`${API_URL}/${id}`, {
        method: "DELETE"
    })
    .then(response => response.json())
    .then(response => {
        if (!response.success) {
            throw new Error(response.message);
        }

        mensagem.textContent = 
            `🗑️ Peça excluída com sucesso!\n` +
            `Código: ${codigo} | Nome: ${nome} | ID: ${id}`;

        mensagem.style.color = "green";

        carregarPecas();
    })
    .catch(error => {
        console.error("Erro ao excluir peça:", error.message);
        mensagem.textContent = "❌ " + error.message;
        mensagem.style.color = "red";
    });
}

