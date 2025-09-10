<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <header>
    <nav class="navbar navbar-expand-lg custom-navbar shadow-sm">
      <div class="container">
        <RouterLink class="navbar-brand fw-bold" to="/">
          <i class="fa-solid fa-address-book me-2"></i>Agenda Telefônica
        </RouterLink>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav ms-auto mb-2 mb-lg-0">
            <li class="nav-item">
              <RouterLink class="nav-link custom-nav-link" to="/">Página Inicial
              </RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link custom-nav-link" to="/agenda">
                <i class="fas fa-address-book me-2"></i>Agenda
              </RouterLink>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  </header>
  <RouterView />
</template>

<style scoped>
.custom-navbar {
  background: linear-gradient(135deg, #ffffff 0%, #f8f9fa 100%);
  border-bottom: 3px solid #007bff;
  padding: 1rem 0;
}

.navbar-brand {
  font-size: 1.5rem;
  color: #007bff !important;
  text-decoration: none;
  transition: color 0.3s ease;
}

.navbar-brand:hover {
  color: #0056b3 !important;
}

.custom-nav-link {
  color: #6c757d !important;
  font-weight: 500;
  padding: 0.5rem 1rem !important;
  margin: 0 0.25rem;
  border-radius: 25px;
  transition: all 0.3s ease;
  text-decoration: none;
}

.custom-nav-link:hover {
  color: #007bff !important;
  background-color: rgba(0, 123, 255, 0.1);
  transform: translateY(-2px);
}

.custom-nav-link.router-link-active {
  color: #007bff !important;
  background-color: rgba(0, 123, 255, 0.15);
  font-weight: 600;
}

.navbar-toggler {
  border: 2px solid #007bff;
  border-radius: 8px;
}

.navbar-toggler:focus {
  box-shadow: 0 0 0 0.2rem rgba(0, 123, 255, 0.25);
}

.navbar-toggler-icon {
  background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 30 30'%3e%3cpath stroke='rgba%280, 123, 255, 1%29' stroke-linecap='round' stroke-miterlimit='10' stroke-width='2' d='M4 7h22M4 15h22M4 23h22'/%3e%3c/svg%3e");
}

/* Responsive adjustments */
@media (max-width: 991px) {
  .custom-nav-link {
    margin: 0.25rem 0;
    text-align: center;
  }
  
  .navbar-nav {
    padding-top: 1rem;
  }
}
</style>
