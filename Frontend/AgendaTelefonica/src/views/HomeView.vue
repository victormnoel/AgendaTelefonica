<template>
  <main>
    <div class="container mt-5">
      <div class="row justify-content-center">
        <div class="col-lg-8 text-center">
          <h1 class="display-4 mb-4">
            <i class="fa-solid fa-address-book text-primary me-3"></i>Agenda Telefônica
          </h1>
          <p class="lead mb-4">
            Bem-vindo ao seu sistema de gerenciamento de contatos. 
            Organize e mantenha todos os seus contatos em um só lugar.
          </p>
          
          <div class="row mt-5">
            <div class="col-md-6 mb-3">
              <div class="card border-primary">
                <div class="card-body">
                  <h5 class="card-title">
                    <i class="fas fa-address-book me-2"></i>Ver Agenda
                  </h5>
                  <p class="card-text">Visualize todos os seus contatos cadastrados</p>
                  <RouterLink to="/agenda" class="btn btn-primary">Acessar Agenda</RouterLink>
                </div>
              </div>
            </div>
            <div class="col-md-6 mb-3">
              <div class="card border-success">
                <div class="card-body">
                  <h5 class="card-title">
                    <i class="fas fa-plus text-success me-2"></i>Novo Contato
                  </h5>
                  <p class="card-text">Adicione um novo contato à sua agenda</p>
                  <RouterLink to="/agenda/criarRegistro" class="btn btn-success">Criar Contato</RouterLink>
                </div>
              </div>
            </div>
          </div>

          <div class="alert alert-info mt-4" role="alert">
            <strong>Dica:</strong> Use a agenda para manter seus contatos sempre organizados e atualizados.
          </div>
        </div>
      </div>
    </div>
  </main>
</template>

<script setup>
import { RouterLink } from 'vue-router'
</script>

<style scoped>
.card {
  transition: transform 0.2s;
}

.card:hover {
  transform: translateY(-5px);
}
</style>
