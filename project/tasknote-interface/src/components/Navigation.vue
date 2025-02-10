<template>
   <div class="mx-auto my-4" style="width: 600px;">
      <v-row cols="4" class="my-2">
        <v-col v-for="item in menu">
          <v-btn
            color="red-lighten-2"   
            @click="routeAccess(item.name)"    
          block>
          {{ item.title }}
          </v-btn>
        </v-col>
      </v-row>
      <v-divider></v-divider>
      <br>
      <router-view></router-view> <!-- This is where the route content will be injected -->
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { useRouter } from 'vue-router';

export default defineComponent({
    setup () {
        
        const router = useRouter();
        const menu = [
                {
                route : '/user',
                name : 'UserList',
                title : 'User'
                },
                {
                route : '/note',
                name : 'NoteList',
                title: 'Note'
                }
            ];
        const routeAccess = (routeName) => {
            return router.push({ name: routeName });
        }
        return {
            menu,
            routeAccess
        }
    }
})
</script>

<style scoped>

</style>