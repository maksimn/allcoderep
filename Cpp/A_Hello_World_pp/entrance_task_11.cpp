#include <iostream>
#include <vector>
#include <string>
#include <algorithm>
#include <cassert>

// Функция сравнения двух строк: возвращает true, если они отличаются ровно в одном символе
bool isEqualButOneChar(const std::string& a, const std::string& b) {
    int n = a.length();
    int count = 0;
    for (int i = 0; i < n; ++i) {
        if (a[i] != b[i]) {
            count++;
        }
    }
    return count == 1;
}

// Рекурсивная функция для поиска восстановленного списка
std::vector<std::string> findRestoredList(const std::vector<std::string>& input, std::vector<std::string> answer, int i) {
    if (i == input.size()) {
        // Проверка условия циклического замыкания (последний с первым)
        return isEqualButOneChar(answer[0], answer[i - 1]) ? answer : std::vector<std::string>();
    }

    const std::string& previous = answer[i - 1];
    std::string current = input[i];
    size_t iq = current.find('?');

    // Пробуем вариант с '0'
    current[iq] = '0';
    if (isEqualButOneChar(current, previous)) {
        answer.push_back(current);
        auto res = findRestoredList(input, answer, i + 1);
        if (!res.empty()) return res;
        answer.pop_back(); // откат
    }

    // Пробуем вариант с '1'
    current[iq] = '1';
    if (isEqualButOneChar(current, previous)) {
        answer.push_back(current);
        auto res = findRestoredList(input, answer, i + 1);
        if (!res.empty()) return res;
    }

    return {};
}

// Перегрузка для начального вызова
std::vector<std::string> findRestoredList(const std::vector<std::string>& input) {
    if (input.empty()) return {};
    
    std::string first = input[0];
    size_t iq = first.find('?');
    
    // Вариант 1: первая строка начинается с '0'
    first[iq] = '0';
    auto result = findRestoredList(input, {first}, 1);
    if (!result.empty()) return result;

    // Вариант 2: первая строка начинается с '1'
    first[iq] = '1';
    return findRestoredList(input, {first}, 1);
}

void readFromStdin() {
    int n;
    if (!(std::cin >> n)) return;
    
    int size = 1 << n;
    std::vector<std::string> input(size);
    for (int i = 0; i < size; ++i) {
        std::cin >> input[i];
    }

    auto result = findRestoredList(input);

    if (result.empty()) {
        std::cout << "NO" << std::endl;
    } else {
        std::cout << "YES" << std::endl;
        for (const auto& s : result) {
            std::cout << s << "\n";
        }
    }
}

void entrance_task_11() {
    // Для ускорения ввода-вывода
    std::ios_base::sync_with_stdio(false);
    std::cin.tie(NULL);

    readFromStdin();
}
