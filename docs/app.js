// ===============================
// CONFIG
// ===============================
const API_URL = "https://pecasantunes-api.onrender.com/api/v1/AutoPecas";

// ===============================
// ELEMENTOS (mantendo seus ids)
// ===============================
const btnCarregar = document.getElementById("btnCarregar");
const listaPecas = document.getElementById("listaPecas");
const formPeca = document.getElementById("formPeca");

const inputCodigo = document.getElementById("codigo");
const inputNome = document.getElementById("nome");
const inputMarca = document.getElementById("marca");
const inputPreco = document.getElementById("preco");
const inputQuantidade = document.getElementById("quantidadeEstoque");
const inputDescricao = document.getElementById("descricao");
const mensagem = document.getElementById("mensagem");

// NOVOS (busca/ordenar/contador/limpar)
const inputBusca = document.getElementById("busca");
const selectOrdenacao = document.getElementById("ordenacao");
const contador = document.getElementById("contador");
const btnLimpar = document.getElementById("btnLimpar");
const btnSalvar = document.getElementById("btnSalvar");

// ===============================
// ESTADO
// ===============================
let pecas = [];
let editId = null;
let linhaEditando = null;

// ===============================
// MOEDA (máscara + conversão)
// ===============================
function moedaParaNumero(valor) {
  if (!valor) return NaN;

  const limpo = valor
    .toString()
    .replace(/\s/g, "")
    .replace("R$", "")
    .replace(/\./g, "")
    .replace(",", ".");

  return parseFloat(limpo);
}

function numeroParaMoeda(valor) {
  const n = Number(valor);
  if (!Number.isFinite(n)) return "";
  return n.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
}

function aplicarMascaraMoedaNoInput(inputEl) {
  inputEl.addEventListener("input", () => {
    const digits = inputEl.value.replace(/\D/g, "");
    if (!digits) {
      inputEl.value = "";
      return;
    }
    const valor = Number(digits) / 100;
    inputEl.value = numeroParaMoeda(valor);
  });

  inputEl.addEventListener("blur", () => {
    const n = moedaParaNumero(inputEl.value);
    if (!Number.isFinite(n)) inputEl.value = "";
  });
}

// ativa máscara no campo preco
aplicarMascaraMoedaNoInput(inputPreco);

// ===============================
// MENSAGENS
// ===============================
function mostrarMensagem(texto, tipo = "sucesso") {
    const container = document.getElementById("toastContainer");

    const toast = document.createElement("div");
    toast.classList.add("toast", tipo);
    toast.textContent = texto;

    container.appendChild(toast);

    setTimeout(() => {
        toast.remove();
    }, 3000);
}

// ===============================
// GET - Listar peças
// ===============================
async function carregarPecas(mostrarMsg = true) {
  btnCarregar.disabled = true;
  btnCarregar.textContent = "Carregando...";

  try {
    const res = await fetch(API_URL);
    const data = await res.json();

    // seu backend retorna { data: [...] }
    pecas = (data && data.data) ? data.data : [];

    renderizarTabela();

    if (mostrarMsg) {
      mostrarMensagem("Peças carregadas com sucesso!", "sucesso");
    }
  } catch (err) {
    console.error("Erro ao carregar peças:", err);
    mostrarMensagem("Erro ao carregar peças!", "erro");
  } finally {
    btnCarregar.disabled = false;
    btnCarregar.textContent = "Carregar peças";
  }
}

// ===============================
// POST / PUT - Salvar peça
// ===============================
async function salvarPeca(e) {
  e.preventDefault();

  const precoNum = moedaParaNumero(inputPreco.value);
  const qtdNum = parseInt(inputQuantidade.value);

  const peca = {
    codigo: inputCodigo.value,
    nome: inputNome.value,
    marca: inputMarca.value,
    preco: Number.isFinite(precoNum) ? precoNum : 0,
    quantidadeEstoque: Number.isFinite(qtdNum) ? qtdNum : 0,
    descricao: inputDescricao.value
  };

  try {
    let res;

    if (editId) {
      // PUT
      res = await fetch(`${API_URL}/${editId}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(peca)
      });

      if (!res.ok) throw new Error("Falha na atualização");
      mostrarMensagem("Peça atualizada!", "sucesso");
    } else {
      // POST
      res = await fetch(API_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(peca)
      });

      if (!res.ok) throw new Error("Falha ao criar");
      mostrarMensagem("Peça criada!", "sucesso");
    }

    // reset UI
    formPeca.reset();
    inputPreco.value = ""; // porque é máscara
    editId = null;

    if (linhaEditando) linhaEditando.classList.remove("editando");
    linhaEditando = null;

    btnSalvar.textContent = "Salvar";

    // recarrega
    await carregarPecas(false);
  } catch (err) {
    console.error("Erro ao salvar peça:", err);
    mostrarMensagem("Erro ao salvar peça!", "erro");
  }
}

// ===============================
// DELETE - Excluir peça
// ===============================
async function excluirPeca(id) {
  if (!confirm("Deseja realmente excluir esta peça?")) return;

  try {
    const res = await fetch(`${API_URL}/${id}`, { method: "DELETE" });
    if (!res.ok) throw new Error("Falha ao excluir");

    mostrarMensagem("Peça excluída com sucesso!", "sucesso");
    await carregarPecas(false);
  } catch (err) {
    console.error("Erro ao excluir peça:", err);
    mostrarMensagem("Erro ao excluir peça!", "erro");
  }
}

// ===============================
// EDITAR - Carregar peça no formulário
// ===============================
function editarPeca(id) {
 const peca = pecas.find(p => p.id == id);
  if (!peca) return;

  editId = peca.id;

  inputCodigo.value = peca.codigo ?? "";
  inputNome.value = peca.nome ?? "";
  inputMarca.value = peca.marca ?? "";
  inputPreco.value = numeroParaMoeda(peca.preco ?? 0);
  inputQuantidade.value = peca.quantidadeEstoque ?? 0;
  inputDescricao.value = peca.descricao ?? "";

  if (linhaEditando) linhaEditando.classList.remove("editando");
  const btn = document.querySelector(`button[onclick="editarPeca('${peca.id}')"]`);
  if (btn) {
    linhaEditando = btn.closest("tr");
    if (linhaEditando) linhaEditando.classList.add("editando");
  }

  // muda botão
  btnSalvar.textContent = "Atualizar";

  mostrarMensagem("Editando peça...", "info");
  formPeca.scrollIntoView({ behavior: "smooth", block: "start" });
}

// ===============================
// STATUS badge
// ===============================
function getStatusEstoque(qtd) {
  const n = Number(qtd || 0);
  if (n <= 0) return { text: "Sem estoque", cls: "badge badge--danger" };
  if (n <= 5) return { text: "Baixo", cls: "badge badge--warn" };
  return { text: "OK", cls: "badge badge--success" };
}

// ===============================
// FILTRO + ORDENAÇÃO (front)
// ===============================
function aplicarFiltroEOrdenacao(lista) {
  const termo = (inputBusca?.value || "").trim().toLowerCase();
  let filtrada = [...lista];

  if (termo) {
    filtrada = filtrada.filter(p => {
      const alvo = `${p.codigo} ${p.nome} ${p.marca} ${p.descricao}`.toLowerCase();
      return alvo.includes(termo);
    });
  }

  const ord = selectOrdenacao?.value || "nenhuma";
  const comparadores = {
    preco_asc: (a, b) => (a.preco ?? 0) - (b.preco ?? 0),
    preco_desc: (a, b) => (b.preco ?? 0) - (a.preco ?? 0),
    estoque_asc: (a, b) => (a.quantidadeEstoque ?? 0) - (b.quantidadeEstoque ?? 0),
    estoque_desc: (a, b) => (b.quantidadeEstoque ?? 0) - (a.quantidadeEstoque ?? 0),
    nome_asc: (a, b) => (a.nome || "").localeCompare(b.nome || ""),
    nome_desc: (a, b) => (b.nome || "").localeCompare(a.nome || "")
  };

  if (comparadores[ord]) filtrada.sort(comparadores[ord]);

  return filtrada;
}

// ===============================
// RENDER - Atualizar tabela
// ===============================
function renderizarTabela() {
  listaPecas.innerHTML = "";

  const listaFinal = aplicarFiltroEOrdenacao(pecas);

  if (contador) {
    contador.textContent = `${listaFinal.length} item(ns) exibido(s)`;
  }

  listaFinal.forEach(p => {
    const tr = document.createElement("tr");
    const st = getStatusEstoque(p.quantidadeEstoque);

    tr.innerHTML = `
      <td>${p.codigo ?? ""}</td>
      <td>${p.nome ?? ""}</td>
      <td>${p.marca ?? ""}</td>
      <td>${Number(p.preco ?? 0).toLocaleString("pt-BR", { style: "currency", currency: "BRL" })}</td>
      <td>${p.quantidadeEstoque ?? 0}</td>
      <td><span class="${st.cls}">${st.text}</span></td>
      <td>${p.descricao ?? ""}</td>
      <td class="acoes">
        <button class="btn btn-success" onclick="editarPeca('${p.id}')">Editar</button>
        <button class="btn btn-danger" onclick="excluirPeca('${p.id}')">Excluir</button>
      </td>
    `;

    listaPecas.appendChild(tr);
  });
}

// ===============================
// EVENTOS
// ===============================
btnCarregar.addEventListener("click", carregarPecas);
formPeca.addEventListener("submit", salvarPeca);

if (inputBusca) inputBusca.addEventListener("input", renderizarTabela);
if (selectOrdenacao) selectOrdenacao.addEventListener("change", renderizarTabela);

if (btnLimpar) {
  btnLimpar.addEventListener("click", () => {
    formPeca.reset();
    inputPreco.value = "";
    editId = null;

    if (linhaEditando) linhaEditando.classList.remove("editando");
    linhaEditando = null;

    btnSalvar.textContent = "Salvar";
    mostrarMensagem("Formulário limpo.", "info");
  });
}

// IMPORTANTE: deixar funções acessíveis pro onclick do HTML gerado
window.editarPeca = editarPeca;
window.excluirPeca = excluirPeca;