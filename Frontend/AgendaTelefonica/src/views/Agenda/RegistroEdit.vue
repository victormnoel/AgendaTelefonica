<template>
  <div class="container mt-5">
    <div class="card">
        <div class="card-header">
            <h4 class="text-center">
                Editar Contato  
            </h4>
            <div v-if="this.mensagemErro" class="alert alert-danger" role="alert">
                {{ this.mensagemErro }}
            </div>
            <div v-if="mensagemSucesso" class="alert alert-success" role="alert">
                {{ mensagemSucesso }}
            </div>
            <ul class="alert alert-warning" v-if="Object.keys(this.listaErros).length > 0">
                <li class="mb-0 ms-3" v-for="(erro, index) in this.listaErros" :key="index">
                    {{erro}}
                </li>
            </ul>
            <div class="mb-3">
                <label for="">Nome</label>
                <input type="text" v-model="modelo.contato.nome" class="form-control"></input>
            </div>
            <div class="mb-3">
                <label for="">Email</label>
                <input type="text" v-model="modelo.contato.email" class="form-control"></input>
            </div>
            <div class="mb-3">
                <label for="">Telefone</label>
                <input type="text" v-model="modelo.contato.telefone" class="form-control"></input>
            </div>
            <button type="button" @click="atualizarContato" class="btn btn-primary float-end">
                    Atualizar
            </button>
        </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
    name: 'RegistroEdit',
    data() {
        return {
            listaErros: '',
            mensagemErro: '',
            mensagemSucesso: '',
            contatoId: '',
            modelo: {
                contato: {
                    nome: '',
                    email: '',
                    telefone: ''
                }
            }
        }
    },
     mounted(){
        this.contatoId = this.$route.params.id;
        this.buscarDadosDoContato(this.contatoId);
    },
    methods: {

      limparMensagens(delay = 0) {
        if (delay > 0) {
            setTimeout(() => {
                this.mensagemErro = '';
                this.mensagemSucesso = '';
            }, delay);
        } else {
            this.mensagemErro = '';
            this.mensagemSucesso = '';
        }
    },

           buscarDadosDoContato(contatoId){

            this.limparMensagens();

            axios.get(`https://localhost:44322/api/Usuario/buscar/${contatoId}`).then(response => {

                this.modelo.contato = response.data;
            }).catch(error => {
                
                if(error.response.status >= 400 && error.response.data !== undefined)
                {
                    this.mensagemErro = error.response.data;
                }
            });
        },
        atualizarContato(){
            
            var aux = this;
            this.limparMensagens();

            axios.put(`https://localhost:44322/api/Usuario/Atualizar/${this.contatoId}`, this.modelo.contato).then(response => {

                this.mensagemSucesso = response.data;
                this.listaErros = '';
                this.$router.push('/agenda');

            }).catch(error => {

                if(error.response.data.erros !== undefined && error.response.data.erros.length > 0)
                {
                    aux.listaErros = error.response.data.erros;
                }
                else{

                    if(error.response.status >= 400 && error.response.data !== undefined)
                    {
                        this.mensagemErro = error.response.data;
                    }
                }
            });
        }
    }
};

</script>
