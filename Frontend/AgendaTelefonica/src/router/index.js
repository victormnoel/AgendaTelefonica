import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import AgendaView from '../views/Agenda/AgendaView.vue'
import RegistroCreate from '../views/Agenda/RegistroCreate.vue'
import RegistroEdit from '../views/Agenda/RegistroEdit.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/agenda',
      name: 'agendaView',
      component: AgendaView,
    },
    {
      path: '/agenda/criarRegistro',
      name: 'registroCreate',
      component: RegistroCreate,
    },
    {
      path: '/agenda/editarRegistro/:id',
      name: 'registroEdit',
      component: RegistroEdit,
      props: true,
    }
  ],
})

export default router
