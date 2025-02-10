<template>
    <div class="mx-auto" style="width: 600px;">
        <v-btn to="/user/create" block>Add User</v-btn>
    </div>
     <v-card
        class="mx-auto my-4"
        elevation="16"
        max-width="1000"
        min-height="150"
        >
        <v-table>
            <thead>
            <tr>
                <th class="text-left">
                ID
                </th>
                <th class="text-center">
                Name
                </th>
                <th class="text-center">Action</th>
            </tr>
            </thead>
            <tbody>
            <tr
                v-for="item in responseData"
                :key="item.userId"
            >
                <td>{{ item.userId }}</td>
                <td class="text-center">{{ item.name }}</td>
                <td class="text-center">
                    <!-- <v-btn @click="messageData(item.userId)">Edit</v-btn>
                    <v-btn @click="deleteItem(item.userId)" color="red" class="ml-2">Delete</v-btn> -->
                    <v-btn @click="messageData(item.userId)">Show Note</v-btn>
                </td>
            </tr>
            </tbody>
        </v-table>
        </v-card>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import axios from 'axios';
import { useRouter } from 'vue-router';  // Import Vue Router's useRouter hook



export default defineComponent({
    name: 'UserList',
    setup () {
        const responseData = ref(null);
        // Use `ref` to make `data` reactive
        const data = ref('hello world');
        const router = useRouter(); 


        const messageData = (id: number) => {
            router.push({ name: 'UserDetail', params: { id } });

        }

        const fetchData = async () => {
            try {
                const response = await axios.get("http://localhost:5001/api/users",{ 
                    headers: {
                            'Authorization' : 'Bearer testing-token',
                            'Accept': 'application/json, text/plain, */*',
                        }
                    }
                );
                responseData.value =  response.data;
            }catch(error){
                console.log(error);
            }
        }
        const deleteItem =  async (id : number) => {
            try {
                await axios.delete(`http://localhost:5001/api/users/${id}`,{ 
                    headers: {
                        'Authorization' : 'Bearer testing-token',
                            'Accept': 'application/json, text/plain, */*',
                        }
                    }
                );
                fetchData();
            } catch (error) {
                
            }
        }

        fetchData();
        return {
            responseData,
            messageData,
            deleteItem
             // this is now reactive and can be used in the template
        };
    }
});
</script>

<style scoped>
/* You can add some styles here */
</style>