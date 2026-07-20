import { useEffect, useState } from "react";

// Define o formato de uma pessoa recebida do backend.
interface Pessoa {
  id: number;
  nome: string;
  idade: number;
}

// Define o formato de uma transação recebida do backend.
interface Transacao {
  id: number;
  descricao: string;
  valor: number;
  tipo: string;
  pessoaId: number;
}

  // Define o formato do resumo financeiro de uma pessoa.
interface ResumoPessoa {
  nome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

// Define o formato do resumo financeiro completo recebido do backend.
interface ResumoFinanceiro {
  pessoas: ResumoPessoa[];
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

function App() {

    // Estado responsável por armazenar a lista de transações vindas do backend.
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);

    // Estado responsável por armazenar a lista de pessoas vindas do backend.
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);

    // Estados responsáveis pelos dados da nova pessoa.
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState("");

    // Estados responsáveis pelos dados da nova transação.
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState("");
  const [tipo, setTipo] = useState("Despesa");
  const [pessoaId, setPessoaId] = useState("");

    // Estado responsável por exibir mensagens de erro ao usuário.
  const [erro, setErro] = useState("");

    // Estado responsável por armazenar o resumo financeiro.
  const [resumo, setResumo] = useState<ResumoFinanceiro>({
    pessoas: [],
    totalReceitas: 0,
    totalDespesas: 0,
    saldo: 0
});

  // Busca todas as pessoas cadastradas no backend
  function buscarPessoas() {

    fetch("http://localhost:5252/api/pessoas")
      .then(res => res.json())
      .then(data => {
        console.log(data);
        setPessoas(data);
    })
      .catch(error => {
        console.log("Erro:", error);
    });

}

// Busca todas as transações cadastradas no backend.
function buscarTransacoes() {

  fetch("http://localhost:5252/api/transacoes")
    .then(res => res.json())
    .then(data => {
      console.log(data);
      setTransacoes(data);
    })
    .catch(error => {
      console.log("Erro:", error);
    });

}

// Busca os totais de receitas, despesas e saldo no backend.
function buscarResumo() {

  fetch("http://localhost:5252/api/transacoes/totais")
    .then(res => res.json())
    .then(data => {
      console.log(data);
      setResumo(data);
    })
    .catch(error => {
      console.log("Erro:", error);
    });

}

  // Envia uma nova pessoa para o backend
function cadastrarPessoa() {

  fetch("http://localhost:5252/api/pessoas", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({
      nome: nome,
      idade: Number(idade)
    })
  })
  .then(res => res.json())
  .then(data => {
    console.log("Pessoa cadastrada:", data);

    // Atualiza a lista de pessoas após o cadastro
    buscarPessoas();

    // Limpa os campos do formulário
    setNome("");
    setIdade("");

  })
  .catch(error => {
    console.log("Erro:", error);
  });

}

  // Envia uma nova transação para o backend.
function cadastrarTransacao() {

  fetch("http://localhost:5252/api/transacoes", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({
      descricao: descricao,
      valor: Number(valor),
      tipo: tipo,
      pessoaId: Number(pessoaId)
    })
  })
  .then(res => {
  if (!res.ok) {
    return res.text().then(msg => {
      throw new Error(msg);
    });
  }

  return res.json();
})

  .then(data => {
    console.log("Transação cadastrada:", data);
    setErro("");

    // Atualiza a lista de transações e resumo financeiro após o cadastro.
    buscarTransacoes();
    buscarResumo();

    // Limpa os campos do formulário
    setDescricao("");
    setValor("");
    setTipo("Despesa");
    setPessoaId("");
  })

  .catch(error => {
    console.log("Erro:", error.message);
    setErro(error.message);
  });

}

   // Executa as buscas iniciais quando a aplicação é carregada.
  useEffect(() => {
     buscarPessoas();
     buscarTransacoes();
     buscarResumo();
  }, []);

  return (
  <div>
    <h1>Controle de Gastos</h1>

    <h2>Cadastrar pessoa:</h2>

<input 
  type="text"
  placeholder="Nome"
  value={nome}
  onChange={(e) => setNome(e.target.value)}
/>

<input
  type="number"
  placeholder="Idade"
  value={idade}
  onChange={(e) => setIdade(e.target.value)}

/>

<button onClick={cadastrarPessoa}>
  Cadastrar
</button>

    <h2>Cadastrar transação:</h2>

    {/* Botão responsável por enviar a nova transação para o backend. */}
  {erro && <p style={{ color: "red" }}>{erro}</p>}

<input
  type="text"
  placeholder="Descrição"
  value={descricao}
  onChange={(e) => setDescricao(e.target.value)}
/>

<input
  type="number"
  placeholder="Valor"
  value={valor}
  onChange={(e) => setValor(e.target.value)}
/>

    <select
  value={tipo}
  onChange={(e) => setTipo(e.target.value)}
>
  <option value="Despesa">Despesa</option>
  <option value="Receita">Receita</option>
</select>

    <select
  value={pessoaId}
  onChange={(e) => setPessoaId(e.target.value)}
>
  <option value="">Selecione uma pessoa</option>

  {pessoas.map((pessoa) => (
    <option key={pessoa.id} value={pessoa.id}>
      {pessoa.nome}
    </option>
  ))}
</select>

  <button onClick={cadastrarTransacao}>
  Cadastrar Transação
</button>

<h2>Resumo Financeiro</h2>

<p>Total de Receitas: R$ {resumo.totalReceitas}</p>

<p>Total de Despesas: R$ {resumo.totalDespesas}</p>

<p>Saldo: R$ {resumo.saldo}</p>

<h3>Resumo por pessoa:</h3>

{resumo.pessoas.map((pessoa) => (
  <div key={pessoa.nome} style={{ marginBottom: "20px" }}>

    <p>
      {pessoa.nome}
    </p>

    <p>
      Receitas: R$ {pessoa.totalReceitas}
    </p>

    <p>
      Despesas: R$ {pessoa.totalDespesas}
    </p>

    <p>
      Saldo: R$ {pessoa.saldo}
    </p>

  </div>
))}

    <h2>Pessoas cadastradas:</h2>

    {/* Percorre a lista de pessoas e exibe os dados na tela. */}
    {pessoas.map((pessoa) => (
      <p key={pessoa.id}>
        {pessoa.nome} - {pessoa.idade} anos
      </p>
    ))}

  <h2>Transações cadastradas:</h2>

    {/* Percorre a lista de transações e exibe os dados na tela. */}
    {transacoes.map((transacao) => (
      <p key={transacao.id}>
        {transacao.descricao} - R$ {transacao.valor} - {transacao.tipo}
      </p>
  ))}

 </div>
  );


}

export default App;