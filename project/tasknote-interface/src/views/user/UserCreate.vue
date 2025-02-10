<template>
   <v-card                     
        elevation="16"
        class="mx-auto"
        width="600"
        min-height="500"
        title="User"
        subtitle="Create"
    >
      <v-form @submit.prevent="submitForm" class="mx-2">
        <v-text-field
          v-model="name"
          label="Name"
        ></v-text-field>
        <v-btn class="mt-2" type="submit" block>Submit</v-btn>
      </v-form>
      </v-card>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import axios from 'axios';
import { useRouter } from 'vue-router';  // Import Vue Router's useRouter hook


export default defineComponent({
    name: 'UserCreate',

    setup () {
        const name = ref('');
        const router = useRouter(); 

        const submitForm = async () => {
            try {
                const response = await axios.post("http://localhost:5001/api/users", 
                    { 
                        headers: {
                            'Authorization' : 'Bearer testing-token',
                            'Accept': 'application/json, text/plain, */*',
                        },
                        name: name.value
                    }
                );
                console.log(response);
                name.value = '';
                router.push({ name: 'UserList' });
            } catch (error) {
                
            }
        }
        return {
            name,
            submitForm
        }
    }
})
</script>

<style scoped>

</style>