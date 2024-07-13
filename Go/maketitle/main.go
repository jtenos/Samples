package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"

	"golang.org/x/text/cases"
	"golang.org/x/text/language"
)

func main() {

	fmt.Println("Enter your lines here (blank line to finish):")

	var lines []string

	for {
		reader := bufio.NewReader(os.Stdin)
		line, _ := reader.ReadString('\n')
		line = strings.TrimSpace(line)
		if line == "" {
			break
		}
		lines = append(lines, line)
	}

	fmt.Println("------------------RESULTS------------------")
	for _, line := range lines {
		fmt.Println(cases.Title(language.English).String(line))
	}
}
