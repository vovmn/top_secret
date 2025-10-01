<template>
  <AuthLayout>
    <v-row class="h-screen pb-16" justify="center">
      <BaseColumn class-list="d-flex justify-center align-center" cols="12">
        <v-card>
          <v-row class="pt-8" justify="center">
            <h1>Вход</h1>
          </v-row>
          <v-card-text>
            <v-form v-model="formValid">
              <v-container height="250" width="350">
                <v-text-field
                  v-model="login"
                  color="info"
                  label="Логин"
                  :rules="[rules.required]"
                  variant="underlined"
                />
                <v-text-field
                  v-model="password"
                  :append-icon="show ? 'mdi-eye' : 'mdi-eye-off'"
                  color="info"
                  hint="Должен содержать минимум 8 символов"
                  label="Пароль"
                  placeholder="Введите пароль"
                  :rules="[rules.required, rules.min]"
                  :type="show ? 'text' : 'password'"
                  variant="underlined"
                  @click:append="show = !show"
                />
              </v-container>

              <v-divider />
              <v-card-actions>
                <v-spacer />
                <v-btn color="success" :disabled="!formValid" @click="onSubmit">
                  Войти
                  <v-icon end icon="mdi-chevron-right" />
                </v-btn>
              </v-card-actions>
            </v-form>
          </v-card-text>
        </v-card>
      </BaseColumn>
    </v-row>
  </AuthLayout>
</template>

<script setup lang="ts">
  import AuthLayout from '@/layouts/AuthLayout.vue'

  const login = ref(null)
  const password = ref(null)

  const show = ref(false)

  const rules = {
    required: (value: string) => !!value || 'Заполните все поля.',
    min: (v: string | any[]) => v.length >= 8 || 'Пароль должен содержать минимум 8 символов',
  }

  const formValid = ref(false)

  function onSubmit () {
    if (formValid.value) {
      console.log('Форма валидна:', { login: login.value, password: password.value })
    }
  }
</script>

<style scoped lang="sass">

</style>
