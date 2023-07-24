<template>
  <div class="container">
    <div class="text-center">
    <h1>Contatos</h1>
    </div>
    <div v-if="exibirFormularioContato">
        <form class="row">
            <div class="mb-3">
              <label for="nomeFormControlInput" class="form-label mt-2">Nome</label>
              <input type="text" class="form-control" id="nomeFormControlInput" placeholder="Nome" v-model="contatoForm.nome">
            </div>
            <div class="mb-3">
              <label for="telefoneFormControlInput" class="form-label  mt-2">Telefone</label>
              <input type="tel" class="form-control" id="telefoneFormControlInput" placeholder="(xx) xxxxx-xxxx" maxlength="11" minlength="10" v-model="contatoForm.telefone">
            </div>
            <div class="mb-3">
              <label for="emailFormControlInput" class="form-label  mt-2">Email</label>
              <input type="email" class="form-control" id="emailFormControlInput" placeholder="exemplo@exemplo.com" v-model="contatoForm.email">
            </div>
        </form>
      <button @click="enviar" class="btn btn-primary from-control">Salvar</button>
      <button @click="cancelar" class="btn btn-secondary from-control">Cancelar</button>
    </div>

    <div v-else>
      <div v-if="contatos.length > 0">
      <table class="table table-bordered">
        <thead>
        <tr>
          <th>Nome</th>
          <th>Telefone</th>
          <th>Email</th>
          <th></th>
        </tr>
        </thead>
        <tbody>

        <tr v-for="contato in contatos" v-bind:key="contato.id">
          <td>{{ contato.nome }}</td>
          <td>{{ contato.telefone }}</td>
          <td>{{ contato.email }}</td>
          <td>
            <span class=" btn btn-secondary" v-on:click="editarFormulario(contato)">Editar</span>
            <button class="btn btn-danger" @click="excluirContato(contato.id)">Remover</button>
          </td>
        </tr>
        </tbody>
      </table>
      </div>

      <div v-else>
        <div class="text-center">
          <h2>Sem contatos salvos</h2>
        </div>
      </div>
      <button class="btn btn-primary" @click="mudarFomulario">Novo contato</button>
    </div>

  </div>
</template>

<script>
import http from "@/http-common";

export default  {
  data() {
    return {
      contatos: [],
      contatoForm: {
        nome: '',
        telefone: '',
        email: '',
      },
      exibirFormularioContato: false,
    };
  },
  mounted() {
    this.listarContatos();
  },
  methods: {
    listarContatos() {
      // Fazer a chamada à API para obter a lista de itens
      http.get('/Contato')
          .then(response => {
            this.contatos = response.data;
          })
          .catch(error => {
            this.erroServidor();
          });
    },
    salvarContato() {
      http.post("/Contato", this.contatoForm)
          .then((response) => {
            console.log("enviado com sucesso");
            console.log(response.data);
            this.listarContatos();
            this.limparFormulario();
            this.mudarFomulario();
          })
          .catch((error) => {
            this.erroServidor();
          });
    },
    editarContato() {
      http.put(`/Contato/${this.contatoForm.id}`, this.contatoForm)
          .then((response) => {
            console.log("enviado com sucesso");
            console.log(response.data);
            this.listarContatos();
            this.limparFormulario();
            this.mudarFomulario();
          })
          .catch((error ) => {
            this.erroServidor();
          });
    },
    excluirContato(id) {
      if(confirm("Você deseja excluir este contato?")) {
        http.delete(`/Contato/${id}`)
            .then((response) => {
              console.log("enviado com sucesso");
              console.log(response.data);
              this.listarContatos();
            })
            .catch((error) => {
              this.this.erroServidor();
            });
      }

    },
    mudarFomulario() {
      this.exibirFormularioContato = !this.exibirFormularioContato;
      console.log(this.exibirFormularioContato);
    },
    editarFormulario(contato) {
      this.contatoForm = contato;
      this.mudarFomulario();
    },
    enviar() {
      if(this.contatoForm.id) {
        this.editarContato()
      }
      else {
        this.salvarContato()
      }
    },
    limparFormulario() {
      this.contatoForm = {
        nome: "",
        telefone: "",
        email: "",
      }
    },
    cancelar() {
      this.limparFormulario();
      this.exibirFormularioContato = false;
    },
    erroServidor() {
      confirm("Erro ao processar a solcitação. Se possível, verifique as informações e tente novamente");
    }
  }
}
</script>
