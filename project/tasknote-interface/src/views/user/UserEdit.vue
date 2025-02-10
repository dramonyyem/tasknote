<template>
    <v-sheet class="mx-auto" width="600">
      <v-form @submit.prevent="updateUser">
        <v-text-field
            label="Name"
            v-model="name"
          ></v-text-field>
        <v-btn class="mt-2" type="submit" block>Submit</v-btn>
      </v-form>
    </v-sheet>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'
import axios from 'axios';
import { useRoute, useRouter } from 'vue-router'

export default defineComponent({
    name: 'UserEdit',

    setup () {
        const route = useRoute();
        const router = useRouter();

        const name = ref('');
        let userId = route.params.id;
        const updateUser = async () => {
            try {
                const response = await axios.put(`http://localhost:5001/api/users/${userId}`, { 
                        name: name.value, 
                    }, { 
                    headers: {
                        'Authorization' : 'Bearer testing',
                        'Accept': 'application/json, text/plain, */*',
                    },
                });
                router.push({ name: 'UserList' });

            } catch (error) {
                
            }
        };
        const getUser = async () => {
            try {
                const response = await axios.get(`http://localhost:5184/api/users/${userId}`, { 
                        headers: {
                            'Authorization' : 'Bearer testing',
                            'Accept': 'application/json, text/plain, */*',
                        },
                });
                name.value = response.data.name

            } catch (error) {
                
            }
        };

        onMounted(() => {
            getUser();
        });
        return {
            name,
            getUser,
            updateUser
        }
    }
})
</script>

<style scoped>

</style>