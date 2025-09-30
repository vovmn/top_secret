<template>
  <AuthLayout>
    <v-row justify="center" class="h-screen pb-16">
      <v-col cols="12" sm="12" md="12" lg="12" class="d-flex justify-center align-center">
        <v-card>
          <v-row justify="center" class="pt-8">
            <h1>Вход</h1>
          </v-row>
          <v-card-text>
            <v-form v-model="formValid">
              <v-container height="250" width="350">
                <v-text-field v-model="login" :rules="[rules.required]" color="info" label="Логин"
                  variant="underlined"></v-text-field>
                <v-text-field v-model="password" hint="Должен содержать минимум 8 символов"
                  :rules="[rules.required, rules.min]" :type="show ? 'text' : 'password'"
                  :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'" @click:append="show = !show" color="info"
                  label="Пароль" placeholder="Введите пароль" variant="underlined"></v-text-field>
              </v-container>

              <v-divider></v-divider>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="success" :disabled="!formValid" @click="onSubmit">
                  Войти
                  <v-icon icon="mdi-chevron-right" end></v-icon>
                </v-btn>
              </v-card-actions>
            </v-form>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </AuthLayout>
</template>

<script setup lang="ts">
import AuthLayout from '@/layouts/AuthLayout.vue';

const login = ref(null)
const password = ref(null)

const show = ref(false)

const rules = {
  required: (value: string) => !!value || 'Заполните все поля.',
  min: (v: string | any[]) => v.length >= 8 || 'Пароль должен содержать минимум 8 символов',
}

const formValid = ref(false)

const onSubmit = () => {
  if (formValid.value) {
    console.log('Форма валидна:', { login: login.value, password: password.value })
  }
}
</script>

<style scoped lang="sass">

</style>
