<template>
  <div class="container mt-5">
    <div class="card">
        <div class="card-header">
            <h4 class="text-center">
                Criar Novo Contato  
            </h4>

            <div v-if="this.mensagemErro" class="alert alert-danger" role="alert">
                {{ this.mensagemErro }}
            </div>
            <ul class="alert alert-warning" v-if="Object.keys(this.listaDErros).length > 0">
                <li class="mb-0 ms-3" v-for="(erro, index) in this.listaDErros" :key="index">
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
            <button type="button" @click="salvarContato" class="btn btn-primary float-end">
                    Salvar
            </button>
        </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
    name: 'RegistroCreate',
    data() {
        return {
            listaDErros: '',
            mensagemErro: '',
            modelo: {
                contato: {
                    nome: '',
                    email: '',
                    telefone: ''
                }
            }
        }
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
        salvarContato(){

            this.limparMensagens(5000)
            var aux = this;
            axios.post('https://localhost:44322/api/Usuario/cadastrar', this.modelo.contato).then(response => {
                this.modelo.contato = { nome: '', email: '', telefone: '' };
                this.$router.push('/agenda');
            }).catch(error => {

                if(error.response.data.erros !== undefined && error.response.data.erros.length > 0)
                {
                    aux.listaDErros = error.response.data.erros;
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
