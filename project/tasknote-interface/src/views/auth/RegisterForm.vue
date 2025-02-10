<template>
    <div style="width: 400px;" class="mx-auto my-4" >
        <v-card min-height="400">
            <div class="text-center">
                <v-icon icon="$vuetify" style="font-size: 80px;" class="my-4"></v-icon>
            </div>

            <v-form @submit.prevent="registerUser" class="mx-4">
                <v-text-field
                    label="Username"
                    v-model="inputData.username"
                ></v-text-field>
                <v-text-field
                    label="Password"
                    v-model="inputData.password"
                ></v-text-field>
                <v-btn
                    class="mt-2"
                    text="Submit"
                    type="submit"
                    block
                ></v-btn>
            </v-form>
        </v-card>
    </div>
</template>

<script lang="ts">
import axios from 'axios';
import { defineComponent, ref } from 'vue'

export default defineComponent({
    setup () {
        const inputData = ref({
            username : '',
            password : '',
        });

        const registerUser = async () => {
            try {
                const response = await axios.post('http://localhost:5001/api/auth', 
                    {
                        username: inputData.value.username,
                        password: inputData.value.password
                    }
                );
                console.log(response);
            } catch (error) {
                
            }
        }
        return {
            inputData,
            registerUser
        }
    }
})
</script>

<style scoped>

</style>