<template>
    <div style="width: 400px;" class="mx-auto my-4" >
        <v-card min-height="400">
            <div class="text-center">
                <v-icon icon="$vuetify" style="font-size: 80px;" class="my-4"></v-icon>
            </div>

            <v-form @submit.prevent="formSubmit" class="mx-4">
                <v-text-field
                    label="Username"
                    v-model="inputData.username"
                    type="text"
                ></v-text-field>
                <v-text-field
                    label="Password"
                    v-model="inputData.password"
                    type="password"
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
import { defineComponent, ref } from 'vue'
import { useRouter } from 'vue-router';

export default defineComponent({
    setup () {
        const inputData = ref({
            username : '',
            password : ''
        });
        const message = ref("");
        const router = useRouter();
        const formSubmit = () => {
            if(inputData.value.username == "daramony" && inputData.value.password == "Mony2001"){
                localStorage.setItem('token', "keyaccess");
                message.value =  "success";
            }else {
                message.value =  "";
            }

            if(message.value == "success"){
                router.push({ name: 'UserList' });
            }
        }
        return {
            formSubmit,
            inputData
        }
    }
})
</script>

<style scoped>

</style>