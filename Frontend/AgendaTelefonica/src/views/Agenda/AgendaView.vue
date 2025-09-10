
<template>
 <div class="container">
    <div class="card">
        <div class="card-header mt-3">
            <h4>
                Agenda
                <RouterLink to="/agenda/criarRegistro" class="btn btn-primary float-end">
                    Novo Registro
                </RouterLink>
            </h4>
        </div>
        <div v-if="this.mensagemErro" class="alert alert-danger" role="alert">
            {{ this.mensagemErro }}
        </div>
        <div v-if="mensagemSucesso" class="alert alert-success" role="alert">
            {{ mensagemSucesso }}
        </div>
        <div class="card-body">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th class="text-center">Nome</th>
                        <th class="text-center">Email</th>
                        <th class="text-center">Telefone</th>
                        <th class="text-center" style="width: 150px;" >Ações</th>
                    </tr>
                </thead>
                 <tbody v-if="this.contatos.length > 0">
                    <tr v-for="(contato, index) in this.contatos" :key="index">
                        <td>{{ contato.nome }}</td>
                        <td>{{ contato.email }}</td>
                        <td>{{ contato.telefone }}</td>
                        <td>
                            <RouterLink :to="{name:'registroEdit', params: {id: contato.id }}" class="btn btn-success btn-sm me-1">
                                Editar
                            </RouterLink>
                            <button type="button" @click="abrirModalExclusao(contato.id, contato.nome)" class="btn btn-danger btn-sm">
                                Excluir
                            </button>
                        </td>
                    </tr>
            </tbody>
            <tbody v-else>
                <tr>
                    <td colspan="5" class="text-center">Nenhum Registro encontrado</td>
                </tr>
            </tbody>
            </table>
        </div>
    </div>

    <div class="modal fade" ref="modalExclusao" id="modalExclusao" tabindex="-1" aria-labelledby="modalExclusaoLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalExclusaoLabel">Confirmar Exclusão</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Tem certeza que deseja excluir o contato <strong>{{ nomeContatoExcluir }}</strong>?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-danger" @click="confirmarExclusao()" data-bs-dismiss="modal">Excluir</button>
                </div>
            </div>
        </div>
    </div>

 </div>
</template>

<script>

import axios from 'axios';
import { Modal } from 'bootstrap';

export default {

 name: 'AgendaView',
 data() {
    return {
        mensagemErro: '',
        mensagemSucesso: '',
        idContatoExcluir: '',
        nomeContatoExcluir: '',
        contatos: []
    }
 },
 mounted() {
    this.buscarContatos();
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

    buscarContatos() {

        this.limparMensagens(5000);

        axios.get('https://localhost:44322/api/Usuario/buscarTodos')
        .then(response => {
            this.contatos = response.data;
        })
        .catch(error => {
            if(error.response.data !== undefined)
            {
                this.mensagemErro = error.response.data;
            }
        });
    },

    abrirModalExclusao(id, nome) {
        this.idContatoExcluir = id;
        this.nomeContatoExcluir = nome;
         const modal = new Modal(this.$refs.modalExclusao);
        modal.show();
    },

    confirmarExclusao() {
        this.limparMensagens();

        axios.patch(`https://localhost:44322/api/Usuario/Remover/${this.idContatoExcluir}`)
        .then(response => {
            this.mensagemSucesso = response.data;
            this.buscarContatos();
        })
        .catch(error => {
            if(error.response.status >= 400 && error.response.data !== undefined)
            {
                this.mensagemErro = error.response.data;
            }
        });
    }
}

}
</script>
