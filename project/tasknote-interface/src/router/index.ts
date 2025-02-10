/**
 * router/index.ts
 *
 * Automatic routes for `./src/pages/*.vue`
 */

// Composables
import { createRouter, createWebHistory } from 'vue-router/auto'
import UserList from '@/views/user/UserList.vue';
import UserCreate from '@/views/user/UserCreate.vue';
import UserDetail from '@/views/user/UserDetail.vue';
import UserEdit from '@/views/user/UserEdit.vue';
import NoteCreate from '@/views/note/NoteCreate.vue';
import NoteList from '@/views/note/NoteList.vue';
import NoteDetail from '@/views/note/NoteDetail.vue';
import NoteEdit from '@/views/note/NoteEdit.vue';
import Navigation from '@/components/Navigation.vue';
import LogInForm from '@/views/auth/LogInForm.vue';
import RegisterForm from '@/views/auth/RegisterForm.vue';
// import { routes } from 'vue-router/auto-routes'


const routes = [
  {
    path : '/login',
    component: LogInForm
  },
  {
    path : "/register",
    component: RegisterForm
  },
  {
    path : '/',
    component: Navigation,
    children : [
      {
        path: 'user',
        name: 'UserList',
        component: UserList,
        meta: { title: 'User', requiresAuth: true },
      },
      {
        path: 'user/create',
        name: 'UserCreate',
        component: UserCreate,
        meta: { title: 'User', requiresAuth: true },
      },
      {
        path: 'user/:id',
        name: 'UserDetail',
        component: UserDetail,
        meta: { title: 'User', requiresAuth: true },

      },
      {
        path: 'user/:id/edit',
        name: 'UserEdit',
        component: UserEdit,
        meta: { title: 'User', requiresAuth: true },

      },
      {
        path: 'note',
        name: 'NoteList',
        component: NoteList,
        meta: { title: 'Note', requiresAuth: true },

      },
      {
        path: 'note/create',
        name: 'NoteCreate',
        component: NoteCreate,
        meta: { title: 'Note', requiresAuth: true },

      },
      {
        path: 'note/:id',
        name: 'NoteDetail',
        component: NoteDetail,
        meta: { title: 'Note', requiresAuth: true },

      },
      {
        path: 'note/:id/edit',
        name: 'NoteEdit',
        component: NoteEdit,
        meta: { title: 'Note', requiresAuth: true },

      },
    ]
  },
  {
    path: '',
    redirect: '/login'
  }

];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})


router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token');
  if (to.meta.requiresAuth && !token) {
    next('/login');
  } else {
    next();
  }
});

// Workaround for https://github.com/vitejs/vite/issues/11804
// router.onError((err, to) => {
//   if (err?.message?.includes?.('Failed to fetch dynamically imported module')) {
//     if (!localStorage.getItem('vuetify:dynamic-reload')) {
//       console.log('Reloading page to fix dynamic import error')
//       localStorage.setItem('vuetify:dynamic-reload', 'true')
//       location.assign(to.fullPath)
//     } else {
//       console.error('Dynamic import error, reloading page did not fix it', err)
//     }
//   } else {
//     console.error(err)
//   }
// })

router.isReady().then(() => {
  localStorage.removeItem('vuetify:dynamic-reload')
})

export default router
